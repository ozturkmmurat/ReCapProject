using Business.Constans;
using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Caching;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheManager _cacheManager;
        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); // Claimleri böl ve _roles dizisne at 
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            // Autofac ile oluşturduğumuz servis mimarisine ulaş 
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var userId = ClaimHelper.GetUserId(_httpContextAccessor.HttpContext);

            if (_cacheManager.Get<IEnumerable<string>>($"{CacheKeys.UserIdForClaim}={userId}") == null)
            {
                throw new SecuredOperationException(UserMessages.TokenExpired);
            }
            var roleClaims = _cacheManager.Get<IEnumerable<string>>($"{CacheKeys.UserIdForClaim}={userId}").ToList();// O an ki kullanıcını Claimroles bul diyoruz 
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role)) // Claimlerin içinde ilgili rol var ise 
                {
                    return; // Metodu çalıştırmaya devam et 
                }
            }
            throw new SecuredOperationException(UserMessages.AuthorizationDenied); // Eğer ki claimi yok ise hata ver 
        }
    }
}