using Domain.Core.Common.Models;
using Money = Domain.Core.Common.ValueObject.Money;

namespace Domain.Core.Order.Entities;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public Money Subtotal {get; private set;}

    private OrderItem(Product.Entities.Product product, int quantity)
    {
        ProductId = product.Id;
        UnitPrice = product.UnitPrice;
        if(quantity > product.Quantity)
            throw new ArgumentException("Order item quantity must be less than or equal to overall item quantity");
        Quantity = quantity;
        Subtotal = UnitPrice with { Amount = UnitPrice.Amount * Quantity };
    }
    
    public static OrderItem Create(Product.Entities.Product product, int quantity)
    {
        return new OrderItem(product, quantity);
    }
}