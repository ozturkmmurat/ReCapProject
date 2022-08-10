using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        //Startup.cs de Middleware yaşam döngüsünde çalıştırıyoruz.
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
           
                app.UseMiddleware<ExceptionMiddleware>(); //Hata yakalama middleware çalıştır.
            
        }
    }
}
