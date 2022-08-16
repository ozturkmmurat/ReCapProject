using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class RefreshToken : MethodInterception
    {


        protected override void OnBefore(IInvocation invocation)
        {
            
        }
    }
}
