using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obaki.DataChecker.Tests.TestHelper
{
    public class OrdersValidator : AbstractValidator<Orders>
    {
        public OrdersValidator()
        {
            RuleFor(orders => orders.Order.Count).GreaterThan(0);
            RuleForEach(x => x.Order).SetValidator(new OrderValidator());
        }
    }

    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.Customer).NotEmpty();
            RuleForEach(x => x.OrderItem).SetValidator(new OrderItemValidator());
        }
    }


    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(orderItem => orderItem.ItemId).NotNull();
        }
    }
}
