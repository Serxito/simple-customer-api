namespace SimpleAPI.Application.Exceptions;

/// <inheritdoc />
public sealed class CustomerNotUpdatedException : Exception
{
    public CustomerNotUpdatedException() : base($"Failed to UPDATE Customer entity!")
    {
    }
}