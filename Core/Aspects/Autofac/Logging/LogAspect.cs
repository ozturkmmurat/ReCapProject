using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Logging;
using Core.CrossCuttingConcers.Logging.Serilog;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Core.Utilities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new ArgumentException(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)ServiceTool.ServiceProvider.GetService(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase?.Info("İşleme başlandı. " + GetLogDetail(invocation));
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            _loggerServiceBase?.Error("İşlem hata aldı: " + e.Message + "" + GetLogDetail(invocation));
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _loggerServiceBase?.Info("İşlem başarıyla bitti. " + GetLogDetail(invocation));
        }

        private string GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name,
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
            };
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Claims.Count() > 0)
            {
                logDetail.User = ClaimHelper.GetUserName(_httpContextAccessor.HttpContext);
                if (logDetail.CustomerId !=0 && logDetail.CustomerId != null)
                {
                    logDetail.CustomerId = ClaimHelper.GetCustomerId(_httpContextAccessor.HttpContext);
                }
            }
            else
            {
                logDetail.User = "UNAUTHORIZE USER";
                logDetail.CustomerId = 0000;
            }

            return JsonConvert.SerializeObject(logDetail);
        }
    }
}
