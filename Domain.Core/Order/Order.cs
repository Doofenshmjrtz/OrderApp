using System.Collections.ObjectModel;
using Domain.Core.Common.Models;
using Domain.Core.Order.Entities;
using Domain.Core.Order.Enums;
using Domain.Core.Order.Events;
using Money = Domain.Core.Common.ValueObject.Money;

namespace Domain.Core.Order;

public class Order : AggregateRoot
{
    private readonly List<OrderItem> _items = [];
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Money OrderTotal { get; private set; }
    public OrderStatus Status { get; private set; }
    public string? CancellationReason { get; private set; }
    public ReadOnlyCollection<OrderItem> Items { get; private set; }

    private Order(Customer.Entities.Customer customer)
    {
        CustomerId = customer.Id;
        OrderDate = DateTime.UtcNow;
        OrderTotal = new Money(_items.First().UnitPrice.Currency, _items.Sum(item => item.Subtotal.Amount));
        Status = OrderStatus.Processing;
        Items = _items.AsReadOnly();
    }

    public static Order Create(Customer.Entities.Customer customer)
    {
        var order = new Order(customer);
        order.Raise(new OrderCreatedDomainEvent(order.Id));
        return order;
    }

    public void AddItem(Product.Entities.Product product, int quantity)
    {
        EnsureOrderIsModifiable();
        var orderItem = OrderItem.Create(product, quantity);
        _items.Add(orderItem);
    }

    public void RemoveItem(Guid orderItemId)
    {
        EnsureOrderIsModifiable();
        var item = _items.FirstOrDefault(i => i.Id == orderItemId);
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException("Can only mark orders as delivered when they are in Processing status");

        if (_items.Count == 0)
            throw new InvalidOperationException("Cannot mark an empty order as delivered");

        Status = OrderStatus.Delivered;
    }

    public void MarkAsCanceled(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Cancellation reason must be provided", nameof(reason));

        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Cannot cancel a delivered order");

        Status = OrderStatus.Canceled;
        CancellationReason = reason;
    }

    private void EnsureOrderIsModifiable()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException("Can only modify orders that are in Processing status");
    }
}