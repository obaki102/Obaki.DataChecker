using Obaki.DataChecker.Services;
using Obaki.DataChecker.Tests.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obaki.DataChecker.Tests.Tests.Sync
{
    public class XmlDataChecker_ValidateXmlDataFromString
    {
        private readonly XmlDataChecker<Orders> _xmlDataChecker;
        public XmlDataChecker_ValidateXmlDataFromString()
        {
            _xmlDataChecker = new XmlDataChecker<Orders>(new OrdersValidator());
        }

        [Fact]
        public void ValidateXmlDataFromString_ValidXmlInput_ShouldBeTrue()
        {
            //Arrange
            string xmlInput = @"<Orders>
                                <Order OrderId=""1"" Customer=""John Doe"">
                                    <OrderItem ItemId=""101"" Description=""Widget"" Quantity=""3"" Price=""10.00"" />
                                    <OrderItem ItemId=""102"" Description=""Gadget"" Quantity=""2"" Price=""15.00"" />
                                </Order>
                                <Order OrderId=""2"" Customer=""Jane Smith"">
                                    <OrderItem ItemId=""201"" Description=""Thingamabob"" Quantity=""1"" Price=""25.00"" />
                                </Order>
                            </Orders>";

            //Act
            var validate = _xmlDataChecker.ValidateXmlDataFromString(xmlInput);

            //Assert
            Assert.True(validate.IsValid);
        }

        [Fact]
        public void ValidateXmlDataFromString_NoOrders_ShouldBeFalse()
        {
            //Arrange
            string xmlInput = @"<Orders>
                            </Orders>";

            //Act
            var validate = _xmlDataChecker.ValidateXmlDataFromString(xmlInput);

            //Assert
            Assert.False(validate.IsValid);
        }

        [Fact]
        public void ValidateXmlDataFromString_CustomerIsEmpty_ShouldBeFalse()
        {
            //Arrange
            string xmlInput = @"<Orders>
                                <Order OrderId=""1"" Customer="""">
                                    <OrderItem ItemId=""101"" Description=""Widget"" Quantity=""3"" Price=""10.00"" />
                                    <OrderItem ItemId=""102"" Description=""Gadget"" Quantity=""2"" Price=""15.00"" />
                                </Order>
                                <Order OrderId=""2"" Customer=""Test"">
                                    <OrderItem ItemId=""201"" Description=""Thingamabob"" Quantity=""1"" Price=""25.00"" />
                                </Order>
                            </Orders>";

            //Act
            var validate = _xmlDataChecker.ValidateXmlDataFromString(xmlInput);

            //Assert
            Assert.False(validate.IsValid);
        }

    }
}
