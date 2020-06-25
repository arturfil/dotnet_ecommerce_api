using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;
using Core.Specifications;

namespace Core.Interfaces
{
  public class OrderByPaymentIntentIdWithItemsSpecification : BaseSpecification<Order>
  {
    public OrderByPaymentIntentIdWithItemsSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
    {
    }
  }
}