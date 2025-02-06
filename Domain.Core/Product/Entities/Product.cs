using Domain.Core.Common.Abstractions;
using Domain.Core.Common.Models;
using Domain.Core.Models.Products;
using Money = Domain.Core.Common.ValueObject.Money;

namespace Domain.Core.Product.Entities;

public class Product : Entity
{
    public string ProductName { get; private set; }
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    private Product(string productName, Money unitPrice, int quantity)
    {
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public static Product Create(string productName, Money unitPrice, int quantity)
    {
        return new Product(productName, unitPrice, quantity);
    }
}