namespace Ordering.Abstraction.Exceptions;

public class OrderCannotBeModifiedException : Exception
{
    public OrderCannotBeModifiedException(string message) : base(message)
    {
    }

    public OrderCannotBeModifiedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}