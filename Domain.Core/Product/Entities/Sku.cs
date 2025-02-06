namespace Domain.Core.Models.Products;

// Stock Keeping Unit
public record Sku
{
    private const int DefaultLength = 10;
    public string Value { get; init; }
    private Sku(string value) => Value = value;

    public static Sku? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        return value.Length != DefaultLength ? null : new Sku(value);
    }
    
}