namespace Ordering.Abstraction.Exceptions;

public class LineIsNotUnique : Exception
{
    public LineIsNotUnique(string message) : base(message)
    {
    }

    public LineIsNotUnique(string message, Exception innerException) : base(message, innerException)
    {
    }
}