using Moq;
using Obaki.DataChecker.Tests.TestHelper;
using System.Text.Json;

namespace Obaki.DataChecker.Tests.Tests.Sync
{
    public class JsonDataChecker_ValidateJsonDataFromString
    {
        private readonly JsonDataChecker<JsonOrders> _jsonDataChecker;
        public JsonDataChecker_ValidateJsonDataFromString()
        {
            _jsonDataChecker = new JsonDataChecker<JsonOrders>(new JsonOrdersValidator());
        }

        [Fact]
        public void ValidateJsonDataFromString_ValidJsonInput_ShouldBeTrue()
        {
            //Arrange
            string JsonInput = @"{
                                ""Orders"": [
                                    {
                                        ""OrderId"": 1,
                                        ""Customer"": ""John Doe"",
                                        ""OrderItem"": [
                                            {
                                                ""ItemId"": 101,
                                                ""Description"": ""Widget"",
                                                ""Quantity"": 3,
                                                ""Price"": 10.00
                                            },
                                            {
                                                ""ItemId"": 102,
                                                ""Description"": ""Gadget"",
                                                ""Quantity"": 2,
                                                ""Price"": 15.00
                                            }
                                        ]
                                    },
                                    {
                                        ""OrderId"": 2,
                                        ""Customer"": ""Jane Smith"",
                                        ""OrderItem"":[ {
                                            ""ItemId"": 201,
                                            ""Description"": ""Thingamabob"",
                                            ""Quantity"": 1,
                                            ""Price"": 5.00
                                        }]
                                    }
                                ]
                            }
                        ";

            //Act
            var validate = _jsonDataChecker.ValidateJsonDataFromString(JsonInput);

            //Assert
            Assert.True(validate.IsValid);
        }

        [Fact]
        public void ValidateJsonDataFromString_NoOrders_ShouldBeFalse()
        {
            //Arrange
            string JsonInput = @"{
                                ""Orders"": []
                            }
                        ";

            //Act
            var validate = _jsonDataChecker.ValidateJsonDataFromString(JsonInput);

            //Assert
            Assert.False(validate.IsValid);
        }

        [Fact]
        public void ValidateJsonDataFromString_CustomerIsEmpty_ShouldBeFalse()
        {
            //Arrange
            string JsonInput = @"{
                                ""Orders"": [
                                    {
                                        ""OrderId"": 1,
                                        ""Customer"":"""" ,
                                        ""OrderItem"": [
                                            {
                                                ""ItemId"": 101,
                                                ""Description"": ""Widget"",
                                                ""Quantity"": 3,
                                                ""Price"": 10.00
                                            },
                                            {
                                                ""ItemId"": 102,
                                                ""Description"": ""Gadget"",
                                                ""Quantity"": 2,
                                                ""Price"": 15.00
                                            }
                                        ]
                                    },
                                    {
                                        ""OrderId"": 2,
                                        ""Customer"": ""Jane Smith"",
                                        ""OrderItem"":[ {
                                            ""ItemId"": 201,
                                            ""Description"": ""Thingamabob"",
                                            ""Quantity"": 1,
                                            ""Price"": 5.00
                                        }]
                                    }
                                ]
                            }
                        ";

            //Act
            var validate = _jsonDataChecker.ValidateJsonDataFromString(JsonInput);

            //Assert
            Assert.False(validate.IsValid);
        }

        [Fact]
        public void ValidateJsonDataFromString_InValidJson_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string JsonInput = "<Invalid Json>";

            //Act
            var action = new Action(() => _jsonDataChecker.ValidateJsonDataFromString(JsonInput));

            //Assert
            Assert.Throws<JsonException>(action);
        }

        [Fact]
        public void ValidateJsonDataFromString_NoJsonInput_ShouldThrowArgumentNullException()
        {
            //Arrange
            string JsonInput = string.Empty;

            //Act
            var action = new Action(() => _jsonDataChecker.ValidateJsonDataFromString(JsonInput));

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ValidateJsonDataFromString_NullValidator_ShouldThrowArgumentNullException()
        {
            //Arrange
            string JsonInput = string.Empty;
            JsonDataChecker<JsonOrders> _nullJsonDataChecker;

            //Act
            var action = new Action(() => _nullJsonDataChecker = new JsonDataChecker<JsonOrders>(null));

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }


        [Fact]
        public void ValidateJsonDataFromString_DeserializedObjectIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            string JsonInput = @"{
                                ""Orders"": [
                                    {
                                        ""OrderId"": 1,
                                        ""Customer"": ""John Doe"",
                                        ""OrderItem"": [
                                            {
                                                ""ItemId"": 101,
                                                ""Description"": ""Widget"",
                                                ""Quantity"": 3,
                                                ""Price"": 10.00
                                            },
                                            {
                                                ""ItemId"": 102,
                                                ""Description"": ""Gadget"",
                                                ""Quantity"": 2,
                                                ""Price"": 15.00
                                            }
                                        ]
                                    },
                                    {
                                        ""OrderId"": 2,
                                        ""Customer"": ""Jane Smith"",
                                        ""OrderItem"":[ {
                                            ""ItemId"": 201,
                                            ""Description"": ""Thingamabob"",
                                            ""Quantity"": 1,
                                            ""Price"": 5.00
                                        }]
                                    }
                                ]
                            }
                        ";

            var mockJsonDataChecker = new Mock<IJsonDataChecker<JsonOrders>>();
            mockJsonDataChecker.Setup(x => x.ValidateJsonDataFromString(It.IsAny<string>())).Throws<ArgumentNullException>();

            //Act
            var action = new Action(() => mockJsonDataChecker.Object.ValidateJsonDataFromString(JsonInput));

            //Assert
            Assert.Throws<ArgumentNullException>(action);

        }
       

        [Fact]
        public void ValidateJsonDataFromString_ValidJsonInputWithExplictValidator_ShouldBeTrue()
        {
            //Arrange
            string JsonInput = @"{
                                ""Orders"": [
                                    {
                                        ""OrderId"": 1,
                                        ""Customer"": ""John Doe"",
                                        ""OrderItem"": [
                                            {
                                                ""ItemId"": 101,
                                                ""Description"": ""Widget"",
                                                ""Quantity"": 3,
                                                ""Price"": 10.00
                                            },
                                            {
                                                ""ItemId"": 102,
                                                ""Description"": ""Gadget"",
                                                ""Quantity"": 2,
                                                ""Price"": 15.00
                                            }
                                        ]
                                    },
                                    {
                                        ""OrderId"": 2,
                                        ""Customer"": ""Jane Smith"",
                                        ""OrderItem"":[ {
                                            ""ItemId"": 201,
                                            ""Description"": ""Thingamabob"",
                                            ""Quantity"": 1,
                                            ""Price"": 5.00
                                        }]
                                    }
                                ]
                            }
                        ";


            var explicitValidator = new JsonOrdersValidator();
            //Act
            var validate = _jsonDataChecker.ValidateJsonDataFromString(JsonInput, explicitValidator);

            //Assert
            Assert.True(validate.IsValid);
        }

        [Fact]
        public void ValidateJsonDataFromString_NoOrdersWithExplicitValidator_ShouldBeFalse()
        {
            //Arrange
            string JsonInput = @"{
                                ""Orders"": []
                            }
                        ";
             var explicitValidator = new JsonOrdersValidator();  

            //Act
            var validate = _jsonDataChecker.ValidateJsonDataFromString(JsonInput, explicitValidator);

            //Assert
            Assert.False(validate.IsValid);
        }

        [Fact]
        public void ValidateJsonDataFromString_CustomerIsEmptyWithExplicitValidator_ShouldBeFalse()
        {
            //Arrange
            string JsonInput = @"{
                                ""Orders"": [
                                    {
                                        ""OrderId"": 1,
                                        ""Customer"":"""" ,
                                        ""OrderItem"": [
                                            {
                                                ""ItemId"": 101,
                                                ""Description"": ""Widget"",
                                                ""Quantity"": 3,
                                                ""Price"": 10.00
                                            },
                                            {
                                                ""ItemId"": 102,
                                                ""Description"": ""Gadget"",
                                                ""Quantity"": 2,
                                                ""Price"": 15.00
                                            }
                                        ]
                                    },
                                    {
                                        ""OrderId"": 2,
                                        ""Customer"": ""Jane Smith"",
                                        ""OrderItem"":[ {
                                            ""ItemId"": 201,
                                            ""Description"": ""Thingamabob"",
                                            ""Quantity"": 1,
                                            ""Price"": 5.00
                                        }]
                                    }
                                ]
                            }
                        ";

            var explicitValidator = new JsonOrdersValidator();

            //Act
            var validate = _jsonDataChecker.ValidateJsonDataFromString(JsonInput, explicitValidator);

            //Assert
            Assert.False(validate.IsValid);
        }

        [Fact]
        public void ValidateJsonDataFromString_InValidJsonWithExpilcitValidator_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string JsonInput = "<Invalid Json>";
            var explicitValidator = new JsonOrdersValidator();

            //Act
            var action = new Action(() => _jsonDataChecker.ValidateJsonDataFromString(JsonInput, explicitValidator));

            //Assert
            Assert.Throws<JsonException>(action);
        }

        [Fact]
        public void ValidateJsonDataFromString_NoJsonInputWithExplicitValidator_ShouldThrowArgumentNullException()
        {
            //Arrange
            string JsonInput = string.Empty;
            var explicitValidator = new JsonOrdersValidator();

            //Act
            var action = new Action(() => _jsonDataChecker.ValidateJsonDataFromString(JsonInput, explicitValidator));

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ValidateJsonDataFromString_ExplicitValidatorIsNull_ShouldThrowArgumentNullException()
        {
            //Arrange
            string JsonInput = string.Empty;

            //Act
            var action = new Action(() => _jsonDataChecker.ValidateJsonDataFromString(JsonInput, null));

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

    }
}
