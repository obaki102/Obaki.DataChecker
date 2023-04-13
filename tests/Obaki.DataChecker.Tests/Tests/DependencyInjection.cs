using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Obaki.DataChecker.Extensions;
using Obaki.DataChecker.Interfaces;
using Obaki.DataChecker.Services;
using Obaki.DataChecker.Tests.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Obaki.DataChecker.Tests.Tests
{
    public class DependencyInjection
    {
        [Fact]
        public void DependencyInjection_CheckIfScopedServiceIsProperlyRegistered_ShouldNotReturnNull()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddDataCheckerAsScoped();
            var serviceProvider = services.BuildServiceProvider();

            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();
        
            // Act
            var myService = serviceProvider.GetService<IXmlDataChecker<XmlOrder>>();
            var scopeService = scope.ServiceProvider.GetService<IXmlDataChecker<XmlOrder>>();

            // Assert
            Assert.NotNull(myService);
            Assert.IsType(typeof(XmlDataChecker<XmlOrder>),myService);
            //Not equals means the service that was injected is a scope service
            Assert.NotEqual(scopeService, myService);
        }

        [Fact]
        public void DependencyInjection_CheckIfSingletonServiceIsProperlyRegistered_ShouldNotReturnNull()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddDataCheckerAsScoped();
            var serviceProvider = services.BuildServiceProvider();

            // Act
            var myService = serviceProvider.GetService<IXmlDataChecker<XmlOrder>>();
            var singletonService = serviceProvider.GetService<IXmlDataChecker<XmlOrder>>();

            // Assert
            Assert.NotNull(myService);
            Assert.IsType(typeof(XmlDataChecker<XmlOrder>), myService);
            //Equals means the service that was injected is a singleton service
            Assert.Equal(singletonService, myService);
        }
    }
}
