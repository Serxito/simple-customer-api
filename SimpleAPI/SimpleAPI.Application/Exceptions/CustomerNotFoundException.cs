namespace SimpleAPI.Application.Exceptions;

/// <inheritdoc />
public sealed class CustomerNotFoundException : Exception
{
    public CustomerNotFoundException() : base($"Customer entity not found!")
    {
    }
}