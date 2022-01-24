using Business.Constans;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); // Claimleri böl ve _roles dizisne at 
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            // Autofac ile oluşturduğumuz servis mimarisine ulaş 
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); // O an ki kullanıcını Claimroles bul diyoruz 
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role)) // Claimlerin içinde ilgili rol var ise 
                {
                    return; // Metodu çalıştırmaya devam et 
                }
            }
            throw new Exception(Messages.AuthorizationDenied); // Eğer ki claimi yok ise hata ver 
        }
    }
}
