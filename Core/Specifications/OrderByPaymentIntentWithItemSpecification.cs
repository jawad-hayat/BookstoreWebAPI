using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;
using Core.Specifications;

namespace Core.Specification
{
    public class OrderByPaymentIntentWithItemSpecification : BaseSpecipication<Order>
    {
        public OrderByPaymentIntentWithItemSpecification(string paymentIntentId) :
            base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}