namespace SimpleAPI.Application.Exceptions;

/// <inheritdoc />
public sealed class CustomerNotCreatedException : Exception
{
    public CustomerNotCreatedException() : base($"Failed to CREATE Customer entity!")
    {
    }
}