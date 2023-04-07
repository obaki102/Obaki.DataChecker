using Obaki.DataChecker.Tests.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obaki.DataChecker.Tests.Sync
{
    public class XmlDataChecker_DeserializeInputString
    {
        private readonly XmlDataChecker _xmlDataChecker;
        public XmlDataChecker_DeserializeInputString()
        {
            _xmlDataChecker = new XmlDataChecker(); 
        }

        [Fact]
        public void DeserializeInputString_ValidXmlInput_ShouldNotBeNull()
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
            var dummyObject = _xmlDataChecker.DeserializeInputString<Orders>(xmlInput);

            //Assert
            Assert.NotNull(dummyObject);
            Assert.Equal(dummyObject.Order[0].Customer, "John Doe");
        }


        [Fact]
        public void DeserializeInputString_InValidXmlInput_ShouldNotBeNullAndInputIsSanitized()
        {
            //Arrange
            string xmlInput = @"<Orders>
                                <Order OrderId=""1"" Customer=""John & % Doe"">
                                    <OrderItem ItemId=""101"" Description=""Widget"" Quantity=""3"" Price=""10.00"" />
                                    <OrderItem ItemId=""102"" Description=""Gadget"" Quantity=""2"" Price=""15.00"" />
                                </Order>
                                <Order OrderId=""2"" Customer=""Jane Smith"">
                                    <OrderItem ItemId=""201"" Description=""Thingamabob"" Quantity=""1"" Price=""25.00"" />
                                </Order>
                            </Orders>";

            //Act
            var dummyObject = _xmlDataChecker.DeserializeInputString<Orders>(xmlInput);

            //Assert
            Assert.NotNull(dummyObject);
            Assert.Equal(dummyObject.Order[0].Customer, "John   Doe");
        }
    }
}
