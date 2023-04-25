using Obaki.DataChecker.Tests.TestHelper;
using System.Text.Json;

namespace Obaki.DataChecker.Tests.Tests.Sync
{
    public class JsonDataChecker_DeserializeInputString
    {
        private readonly JsonDataChecker<JsonOrders> _JsonDataChecker;
        public JsonDataChecker_DeserializeInputString()
        {
            _JsonDataChecker = new JsonDataChecker<JsonOrders>(new JsonOrdersValidator());
        }

        [Fact]
        public void DeserializeInputString_ValidJsonInput_ShouldNotBeNull()
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
            var dummyObject = _JsonDataChecker.DeserializeInputString(JsonInput);

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
            var action = new Action(() => _JsonDataChecker.DeserializeInputString(JsonInput));

            //Assert
            Assert.Throws<ArgumentNullException>(() => action.Invoke());
        }

        [Fact]
        public void DeserializeInputString_InValidJson_ShouldThrowInvalidOperationException()
        {
            //Arrange
            string JsonInput = "<Invalid Json>";
            //Act
            var action = new Action(() => _JsonDataChecker.DeserializeInputString(JsonInput));

            //Assert
            Assert.Throws<JsonException>(() => action.Invoke());
        }
    }
}
