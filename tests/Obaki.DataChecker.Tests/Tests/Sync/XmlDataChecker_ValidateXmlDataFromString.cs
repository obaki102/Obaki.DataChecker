using Moq;
using Obaki.DataChecker.Interfaces;
using Obaki.DataChecker.Services;
using Obaki.DataChecker.Tests.TestHelper;
using System.Net.Http;

namespace Obaki.DataChecker.Tests.Tests.Sync
{
    public class XmlDataChecker_ValidateXmlDataFromString
    {
        private readonly XmlDataChecker<XmlOrders> _xmlDataChecker;
        public XmlDataChecker_ValidateXmlDataFromString()
        {
            _xmlDataChecker = new XmlDataChecker<XmlOrders>(new XmlOrdersValidator());
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
        public void ValidateXmlDataFromString_ValidXmlInputAndValidator_ShouldBeTrue()
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

            var dummyValidator = new XmlOrdersValidator();

            //Act
            var validate = _xmlDataChecker.ValidateXmlDataFromString(xmlInput, dummyValidator);

            //Assert
            Assert.True(validate.IsValid);
        }

        [Fact]
        public void ValidateXmlDataFromString_ValidXmlInputAndNullValidator_ShouldThrowArgumentNullException()
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
            var action = new Action(() => _xmlDataChecker.ValidateXmlDataFromString(xmlInput,null));

            //Assert
            Assert.Throws<ArgumentNullException>(action);
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
        public void ValidateXmlDataFromString_NoOrdersWithValidator_ShouldBeFalse()
        {
            //Arrange
            string xmlInput = @"<Orders>
                            </Orders>";

            var dummyValidator = new XmlOrdersValidator();

            //Act
            var validate = _xmlDataChecker.ValidateXmlDataFromString(xmlInput, dummyValidator);

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

        [Fact]
        public void ValidateXmlDataFromString_CustomerIsEmptyWithValidator_ShouldBeFalse()
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
            var dummyValidator = new XmlOrdersValidator();

            //Act
            var validate = _xmlDataChecker.ValidateXmlDataFromString(xmlInput, dummyValidator);

            //Assert
            Assert.False(validate.IsValid);
        }

        [Fact]
        public void ValidateXmlDataFromString_InValidXml_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string xmlInput = "<Invalid XML>";

            //Act
            var action = new Action(() => _xmlDataChecker.ValidateXmlDataFromString(xmlInput));

            //Assert
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void ValidateXmlDataFromString_InValidXmlWithValidator_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string xmlInput = "<Invalid XML>";
            var dummyValidator = new XmlOrdersValidator();

            //Act
            var action = new Action(() => _xmlDataChecker.ValidateXmlDataFromString(xmlInput, dummyValidator));

            //Assert
            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void ValidateXmlDataFromString_NoXmlInput_ShouldThrowArgumentNullException()
        {
            //Arrange
            string xmlInput = string.Empty;

            //Act
            var action = new Action(() => _xmlDataChecker.ValidateXmlDataFromString(xmlInput));

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ValidateXmlDataFromString_NoXmlInputWithValidator_ShouldThrowArgumentNullException()
        {
            //Arrange
            string xmlInput = string.Empty;
            var dummyValidator = new XmlOrdersValidator();

            //Act
            var action = new Action(() => _xmlDataChecker.ValidateXmlDataFromString(xmlInput, dummyValidator));

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ValidateXmlDataFromString_NullValidator_ShouldThrowArgumentNullException()
        {
            //Arrange
            string xmlInput = string.Empty;
            XmlDataChecker<XmlOrders> _nullXmlDataChecker;

            //Act
            var action = new Action(() => _nullXmlDataChecker = new XmlDataChecker<XmlOrders>(null));

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }


        [Fact]
        public void ValidateXmlDataFromString_DeserializedObjectIsNull_ShouldThrowArgumentNullException()
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

            var mockXmlDataChecker = new Mock<IXmlDataChecker<XmlOrders>>();
            mockXmlDataChecker.Setup(x => x.ValidateXmlDataFromString(It.IsAny<string>())).Throws<ArgumentNullException>();

            //Act
            var action = new Action(() => mockXmlDataChecker.Object.ValidateXmlDataFromString(xmlInput));

            //Assert
            Assert.Throws<ArgumentNullException>(action);

        }

    }
}
