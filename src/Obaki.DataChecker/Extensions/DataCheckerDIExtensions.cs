using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Obaki.DataChecker.Interfaces;
using Obaki.DataChecker.Services;
using System.Reflection;

namespace Obaki.DataChecker.Extensions
{
    public static class DataCheckerDIExtensions
    {
        public static IServiceCollection AddDataCheckerAsScoped(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddScoped(typeof(IXmlDataChecker<>), typeof(XmlDataChecker<>));
            services.AddScoped(typeof(IJsonDataChecker<>), typeof(JsonDataChecker<>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddDataCheckerAsScopedAsSingleton(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddSingleton(typeof(IXmlDataChecker<>), typeof(XmlDataChecker<>));
            services.AddSingleton(typeof(IJsonDataChecker<>), typeof(JsonDataChecker<>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
