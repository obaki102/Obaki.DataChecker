using Moq;
using Obaki.DataChecker.Interfaces;
using Obaki.DataChecker.Services;
using Obaki.DataChecker.Tests.TestHelper;

namespace Obaki.DataChecker.Tests.Tests.Async
{
    public class XmlDataChecker_ValidateXmlDataFromStringAsync
    {
        private readonly XmlDataChecker<Orders> _xmlDataChecker;
        public XmlDataChecker_ValidateXmlDataFromStringAsync()
        {
            _xmlDataChecker = new XmlDataChecker<Orders>(new OrdersValidator());
        }

        [Fact]
        public async Task ValidateXmlDataFromStringAsync_ValidXmlInput_ShouldBeTrue()
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
            var result = await _xmlDataChecker.ValidateXmlDataFromStringAsync(xmlInput);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task ValidateXmlDataFromStringAsync_NoOrders_ShouldBeFalse()
        {
            //Arrange
            string xmlInput = @"<Orders>
                            </Orders>";

            //Act
            var result = await _xmlDataChecker.ValidateXmlDataFromStringAsync(xmlInput);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task ValidateXmlDataFromStringAsync_CustomerIsEmpty_ShouldBeFalse()
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
            var result = await _xmlDataChecker.ValidateXmlDataFromStringAsync(xmlInput);

            //Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void ValidateXmlDataFromStringAsync_InValidXml_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string xmlInput = "<Invalid XML>";
            //Act
            
            var function = new Func<Task>(async () => await _xmlDataChecker.ValidateXmlDataFromStringAsync(xmlInput));

           //Assert
           Assert.ThrowsAsync<InvalidOperationException>(function);
        }

        [Fact]
        public void ValidateXmlDataFromStringAsync_NoXmlInput_ShouldThrowArgumentNullException()
        {
            //Arrange
            string xmlInput = string.Empty;

            //Act
            var function = new Func<Task>(async () => await _xmlDataChecker.ValidateXmlDataFromStringAsync(xmlInput));

            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(function);
        }

        [Fact]
        public void ValidateXmlDataFromStringAsync_NullValidator_ShouldThrowArgumentNullException()
        {
            //Arrange
            string xmlInput = string.Empty;
            XmlDataChecker<Orders> _nullXmlDataChecker;

            //Act
            var function = new Func<Task>(async () => await _xmlDataChecker.ValidateXmlDataFromStringAsync(xmlInput));

            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(function);
        }


        [Fact]
        public void ValidateXmlDataFromStringAsync_DeserializedObjectIsNull_ShouldThrowArgumentNullException()
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

            var mockXmlDataChecker = new Mock<IXmlDataChecker<Orders>>();
            mockXmlDataChecker.Setup(x => x.ValidateXmlDataFromStringAsync(It.IsAny<string>())).Throws<ArgumentNullException>();
            //Act
            var function = new Func<Task>(async () => await mockXmlDataChecker.Object.ValidateXmlDataFromStringAsync(xmlInput));

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(function);

        }

    }
}
