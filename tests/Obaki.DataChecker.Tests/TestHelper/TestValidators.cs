using FluentValidation;

namespace Obaki.DataChecker.Tests.TestHelper
{
    public class XmlOrdersValidator : AbstractValidator<XmlOrders>
    {
        public XmlOrdersValidator()
        {
            RuleFor(orders => orders.Orders.Count).GreaterThan(0);
            RuleForEach(x => x.Orders).SetValidator(new XmlOrderValidator());
        }
    }

    public class XmlOrderValidator : AbstractValidator<XmlOrder>
    {
        public XmlOrderValidator()
        {
            RuleFor(order => order.Customer).NotEmpty();
            RuleForEach(x => x.OrderItem).SetValidator(new XmlOrderItemValidator());
        }
    }

    public class XmlOrderItemValidator : AbstractValidator<XmlOrderItem>
    {
        public XmlOrderItemValidator()
        {
            RuleFor(orderItem => orderItem.ItemId).NotNull();
        }
    }


    public class JsonOrdersValidator : AbstractValidator<JsonOrders>
    {
        public JsonOrdersValidator()
        {
            RuleFor(orders => orders.Orders.Count).GreaterThan(0);
            RuleForEach(x => x.Orders).SetValidator(new JsonOrderValidator());
        }
    }

    public class JsonOrderValidator : AbstractValidator<JsonOrder>
    {
        public JsonOrderValidator()
        {
            RuleFor(order => order.Customer).NotEmpty();
            RuleForEach(x => x.OrderItem).SetValidator(new JsonOrderItemValidator());
        }
    }

    public class JsonOrderItemValidator : AbstractValidator<JsonOrderItem>
    {
        public JsonOrderItemValidator()
        {
            RuleFor(orderItem => orderItem.ItemId).NotNull();
        }
    }
}
