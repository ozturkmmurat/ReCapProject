using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    // Attribute Classlar, Metodlar ve , Birden fazla kez kullanılabilir olarak belirtiyoruz 
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; } // Öncelik anlamına geliyor Hangi attribute önce çalışacağını belirtebiliriz.

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}