using OrderService.Domain.Comman.Exceptions;
namespace OrderService.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        if (amount < 0)
            throw new DomainException("Money amount cannot be negative");
        Amount = amount;
    }

    public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount);
    public static Money operator *(Money a, int quantity) => new(a.Amount * quantity);
}