using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)  // Veri 60 dakika boyunca önbellekte tutulacak
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            // Namespace ve İnterface veya Class adı  | Çalıştırılacak olan metodun ismi
            var arguments = invocation.Arguments.ToList(); // Metodun parametlerini listeye çevir 
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            // Metodun paramteresini de namespace ekle   parametresi yok ise null ver 
            // String joinden sonrası --> bir araya getir aralarına virgül koy  parametlerin her biri için , koy
            //  x?.ToString() ?? "<Null>") parametre null değil ise  bunu ekle null ise bunu ekle  ?? işaretinin anlamı
            // var ise bunu ekle yok ise diğerini 
            if (_cacheManager.IsAdd(key)) // Böyle bir cache bellekte var mı kontrol et diyoruz.
            {
                invocation.ReturnValue = _cacheManager.Get(key); // Metodu çalıştırmadan bellektekini çalıştır 
                return;
            }
            invocation.Proceed(); // Metodu çalıştırmaya devam ettir yok ise 
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // Cache oluştur 
        }
    }
}
