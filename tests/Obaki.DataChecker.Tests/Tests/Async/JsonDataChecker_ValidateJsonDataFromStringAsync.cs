using Moq;
using Obaki.DataChecker.Interfaces;
using Obaki.DataChecker.Services;
using Obaki.DataChecker.Tests.TestHelper;
using System.Text.Json;

namespace Obaki.DataChecker.Tests.Tests.Async
{
    public class JsonDataChecker_ValidateJsonDataFromStringAsync
    {
        private readonly JsonDataChecker<JsonOrders> _jsonDataChecker;
        public JsonDataChecker_ValidateJsonDataFromStringAsync()
        {
            _jsonDataChecker = new JsonDataChecker<JsonOrders>(new JsonOrdersValidator());
        }

        [Fact]
        public async Task ValidateJsonDataFromString_ValidJsonInput_ShouldBeTrue()
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
            var validate = await _jsonDataChecker.ValidateJsonDataFromStringAsync(JsonInput);

            //Assert
            Assert.True(validate.IsValid);
        }

        [Fact]
        public async Task ValidateJsonDataFromString_NoOrders_ShouldBeFalse()
        {
            //Arrange
            string JsonInput = @"{
                                ""Orders"": []
                            }
                        ";

            //Act
            var validate = await _jsonDataChecker.ValidateJsonDataFromStringAsync(JsonInput);

            //Assert
            Assert.False(validate.IsValid);
        }

        [Fact]
        public async Task ValidateJsonDataFromString_CustomerIsEmpty_ShouldBeFalse()
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
            var validate = await _jsonDataChecker.ValidateJsonDataFromStringAsync(JsonInput);

            //Assert
            Assert.False(validate.IsValid);
        }

        [Fact]
        public void ValidateJsonDataFromString_InValidJson_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string JsonInput = "<Invalid Json>";

            //Act
            var function = new Func<Task>(async () => await _jsonDataChecker.ValidateJsonDataFromStringAsync(JsonInput));

            //Assert
            Assert.ThrowsAsync<JsonException>(function);
        }

        [Fact]
        public void ValidateJsonDataFromString_NoJsonInput_ShouldThrowArgumentNullException()
        {
            //Arrange
            string JsonInput = string.Empty;

            //Act
            var function = new Func<Task>(async () => await _jsonDataChecker.ValidateJsonDataFromStringAsync(JsonInput));

            //Assert
            Assert.ThrowsAsync<JsonException>(function);
        }

        [Fact]
        public void ValidateJsonDataFromString_NullValidator_ShouldThrowArgumentNullException()
        {
            //Arrange
            string JsonInput = string.Empty;

            //Act
            var function = new Func<Task>(async () => await _jsonDataChecker.ValidateJsonDataFromStringAsync(JsonInput));

            //Assert
            Assert.ThrowsAsync<JsonException>(function);
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
            mockJsonDataChecker.Setup(x => x.ValidateJsonDataFromStringAsync(It.IsAny<string>())).Throws<ArgumentNullException>();
           
            //Act
            var function = new Func<Task>(async () => await mockJsonDataChecker.Object.ValidateJsonDataFromStringAsync(JsonInput));

            //Assert
            Assert.ThrowsAsync<JsonException>(function);

        }
    }
}
