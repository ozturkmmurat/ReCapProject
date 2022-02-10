using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class ServiseCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers 
            // Startup bölümünde 
            // ICoreModule dışında başka module ile de çalışmak istediğimde 
           // Birden fazla module eklememi sağlıyor bu bölümdeki kod
           (this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}

