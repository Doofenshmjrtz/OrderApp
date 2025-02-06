using Domain.Core.Models.Products;
using Money = Domain.Core.Common.ValueObject.Money;

namespace Domain.Core;

public class OrderTransactionMiddleman
{
    public bool ReviewTransaction(Order.Order order, Customer.Entities.Customer customer)
    {
        // Check funds first
        if (!customer.HasSufficientFunds(new Money("GEL",order.OrderTotal.Amount)))
        {
            order.MarkAsCanceled("Insufficient funds");
            return false;
        }

        // Get manual approval from console
        Console.WriteLine("\n=== Transaction Review ===");
        Console.WriteLine("Do you approve this transaction? (y/n):");

        var isApproved = Console.ReadLine()?.ToLower() == "y";

        if (isApproved)
        {
            customer.DeductBalance(new Money("GEL",order.OrderTotal.Amount));
            order.MarkAsDelivered();
            return true;
        }

        order.MarkAsCanceled("Transaction rejected by middleman");
        return false;
    }
}