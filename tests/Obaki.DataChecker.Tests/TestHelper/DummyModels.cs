using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Obaki.DataChecker.Tests.TestHelper
{
    [XmlRoot(ElementName = "OrderItem")]
    public class XmlOrderItem
    {

        [XmlAttribute(AttributeName = "ItemId")]
        public int ItemId { get; set; }

        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "Quantity")]
        public int Quantity { get; set; }

        [XmlAttribute(AttributeName = "Price")]
        public double Price { get; set; }
    }

    [XmlRoot(ElementName = "Order")]
    public class XmlOrder
    {

        [XmlElement(ElementName = "OrderItem")]
        public List<XmlOrderItem> OrderItem { get; set; } = new();

        [XmlAttribute(AttributeName = "OrderId")]
        public int OrderId { get; set; }

        [XmlAttribute(AttributeName = "Customer")]
        public string Customer { get; set; } =string.Empty;
    }

    [XmlRoot(ElementName = "Orders")]
    public class XmlOrders
    {

        [XmlElement(ElementName = "Order")]
        public List<XmlOrder> Orders { get; set; } = new();
    }


    public class JsonOrderItem
    {

        [JsonPropertyName("ItemId")]
        public int ItemId { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("Price")]
        public double Price { get; set; }
    }

    public class JsonOrder
    {
        [JsonPropertyName("OrderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("OrderItem")]
        public List<JsonOrderItem> OrderItem { get; set; } = new();

        [JsonPropertyName("Customer")]
        public string Customer { get; set; } = string.Empty;
    }

    public class JsonOrders
    {
        [JsonPropertyName("Orders")]
        public List<JsonOrder> Orders { get; set; } = new();
    }
}
