using Obaki.DataChecker.Services;
using Obaki.DataChecker.Tests.TestHelper;

namespace Obaki.DataChecker.Tests.Sync
{
    public class XmlDataChecker_DeserializeInputString
    {
        private readonly XmlDataChecker<XmlOrders> _xmlDataChecker;
        public XmlDataChecker_DeserializeInputString()
        {
            _xmlDataChecker = new XmlDataChecker<XmlOrders>(new XmlOrdersValidator()); 
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
            var dummyObject = _xmlDataChecker.DeserializeInputString(xmlInput);

            //Assert
            Assert.NotNull(dummyObject);
            Assert.Equal("John Doe", dummyObject.Orders[0].Customer);
        }

        [Fact]
        public void DeserializeInputString_InValidXmlInput_ShouldNotBeNull()
        {
            //Arrange
            string xmlInput = @"<Orders>
                                <Order OrderId=""1"" Customer=""John &amp; Doe"">
                                    <OrderItem ItemId=""101"" Description=""Widget"" Quantity=""3"" Price=""10.00"" />
                                    <OrderItem ItemId=""102"" Description=""Gadget"" Quantity=""2"" Price=""15.00"" />
                                </Order>
                                <Order OrderId=""2"" Customer=""Jane Smith"">
                                    <OrderItem ItemId=""201"" Description=""Thingamabob"" Quantity=""1"" Price=""25.00"" />
                                </Order>
                            </Orders>";

            //Act
            var dummyObject = _xmlDataChecker.DeserializeInputString(xmlInput);

            //Assert
            Assert.NotNull(dummyObject);
            Assert.Equal("John & Doe", dummyObject.Orders[0].Customer);
            Assert.True(dummyObject.Orders[0].Customer.Contains('&'));
        }

        [Fact]
        public void DeserializeInputString_NoXmlInput_ShouldThrowArgumentNullException()
        {
            //Arrange
            string xmlInput = string.Empty;
            //Act
            var action = new Action(()=> _xmlDataChecker.DeserializeInputString(xmlInput));

            //Assert
            Assert.Throws<ArgumentNullException>(()=> action.Invoke());
        }

        [Fact]
        public void DeserializeInputString_InValidXml_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string xmlInput = "<Invalid XML>";
            //Act
            var action = new Action(() => _xmlDataChecker.DeserializeInputString(xmlInput));

            //Assert
            Assert.Throws<InvalidOperationException>(() => action.Invoke());
        }
    }
}
