using System.Text.RegularExpressions;

namespace Domain.Core.Common.ValueObject;

public sealed partial record Money
{
    public string Currency { get; }
    public decimal Amount { get; set; }
    public Money(string currency, decimal? amount)
    {
        ArgumentException.ThrowIfNullOrEmpty(currency);
        if (!CurrencyRegex().IsMatch(currency))
            throw new ArgumentException($"'{currency}' is not a valid currency");
        Currency = currency;
        Amount = amount ?? 0;
    }

    [GeneratedRegex("^[A-Z]{3}$")]
    private static partial Regex CurrencyRegex();
};