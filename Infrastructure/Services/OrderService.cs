using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
  public class OrderService : IOrderService
  {
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
    {
      this._unitOfWork = unitOfWork;
      this._basketRepo = basketRepo;
    }

    /**
      * This method will create an order in the following way:
        - get deliver method from repo
        - calculate subtotal
        - create order
        - save to db
        - return order
     */
    public async Task<Order> CreatOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
      var basket = await _basketRepo.GetBasketAsync(basketId);
      var items = new List<OrderItem>();
      foreach (var item in basket.Items)
      {
        var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
        var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
        var OrderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
        items.Add(OrderItem);
      }
      var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
      var subtotal = items.Sum(items => items.Price * items.Quantity);
      var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
      _unitOfWork.Repository<Order>().Add(order);

      var results = await _unitOfWork.Complete();

      if (results <= 0) return null;

      await _basketRepo.DeleteBasketAsync(basketId);
      return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
      return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
      var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);
      return await _unitOfWork.Repository<Order>().GetEntitiyWithSpec(spec);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
      var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
      return await _unitOfWork.Repository<Order>().ListAsync(spec);
    }
  }
}