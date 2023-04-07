using System.Xml.Serialization;

namespace Obaki.DataChecker.Tests.TestHelper
{
    [XmlRoot(ElementName = "OrderItem")]
    public class OrderItem
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
    public class Order
    {

        [XmlElement(ElementName = "OrderItem")]
        public List<OrderItem> OrderItem { get; set; } = new();

        [XmlAttribute(AttributeName = "OrderId")]
        public int OrderId { get; set; }

        [XmlAttribute(AttributeName = "Customer")]
        public string Customer { get; set; } =string.Empty;
    }

    [XmlRoot(ElementName = "Orders")]
    public class Orders
    {

        [XmlElement(ElementName = "Order")]
        public List<Order> Order { get; set; } = new();
    }
}
