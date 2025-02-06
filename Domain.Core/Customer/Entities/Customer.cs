using Domain.Core.Common.Models;
using Domain.Core.Customer.ValueObjects;
using Money = Domain.Core.Common.ValueObject.Money;

namespace Domain.Core.Customer.Entities;

public class Customer : Entity
{
    public Name Name { get; private set; }
    public HashSet<Money> Balance { get; private set; }

    private Customer(Name name, HashSet<Money> initialBalance)
    {
        Name = name;
        Balance = initialBalance;
    }

    public static Customer Create(string firstName, string lastName, HashSet<Money> initialBalance)
    {
        return new Customer(new Name(firstName, lastName), initialBalance);
    }

    public bool HasSufficientFunds(Money value)
    {
        foreach (var balance in Balance.Where(balance => balance.Currency.Equals(value.Currency)))
        {
            return balance.Amount >= value.Amount;
        }
        throw new InvalidOperationException("No such currency");
    }

    public void DeductBalance(Money value)
    {
        if (!HasSufficientFunds(value))
            throw new InvalidOperationException("Insufficient funds");

        foreach (var balance in Balance.Where(balance => balance.Currency.Equals(value.Currency)))
        {
            balance.Amount -= value.Amount;
        }
    }
}