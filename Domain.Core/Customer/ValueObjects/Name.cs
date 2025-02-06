namespace Domain.Core.Customer.ValueObjects;

public sealed record Name
{
    public string FirstName { get; }
    public string LastName { get; }

    public Name(string firstName, string lastName)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        FirstName = firstName;
        ArgumentException.ThrowIfNullOrEmpty(lastName);
        LastName = lastName;
        // Can be added additional checks for attributes
    }
}