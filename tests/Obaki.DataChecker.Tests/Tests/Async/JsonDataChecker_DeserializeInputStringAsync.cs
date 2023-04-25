using Obaki.DataChecker.Tests.TestHelper;
using System.Text.Json;

namespace Obaki.DataChecker.Tests.Tests.Async
{
    public class JsonDataChecker_DeserializeInputStringAsync
    {
        private readonly JsonDataChecker<JsonOrders> _jsonDataChecker;
        public JsonDataChecker_DeserializeInputStringAsync()
        {
            _jsonDataChecker = new JsonDataChecker<JsonOrders>(new JsonOrdersValidator());
        }

        [Fact]
        public async Task DeserializeInputStringAsync_ValidJsonInput_ShouldNotBeNull()
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
            var dummyObject = await _jsonDataChecker.DeserializeInputStringAsync(JsonInput);

            //Assert
            Assert.NotNull(dummyObject);
            Assert.Equal("John Doe", dummyObject.Orders[0].Customer);
            Assert.True(!dummyObject.Orders[0].Customer.Contains('&'));
            Assert.True(!dummyObject.Orders[0].Customer.Contains('%'));
        }

        [Fact]
        public void DeserializeInputString_NoJsonInput_ShouldThrowArgumentNullException()
        {
            //Arrange
            string JsonInput = string.Empty;

            //Act
            var function = new Func<Task>(async () => await _jsonDataChecker.DeserializeInputStringAsync(JsonInput));

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => function.Invoke());
        }

        [Fact]
        public void DeserializeInputString_InValidJson_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string JsonInput = "<Invalid Json>";

            //Act
            var function = new Func<Task>(async () => await _jsonDataChecker.DeserializeInputStringAsync(JsonInput));

            //Assert
            Assert.ThrowsAsync<JsonException>(() => function.Invoke());

        }

        [Fact]
        public void DeserializeInputString_InValidJson2_ShouldThrowInvalidOperationException()
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
            var function = new Func<Task>(async () => await _jsonDataChecker.DeserializeInputStringAsync(JsonInput));

            //Assert
            Assert.ThrowsAsync<JsonException>(() => function.Invoke());

        }
    }
}