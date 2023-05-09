namespace SimpleAPI.Domain.Exceptions;

public class CustomerNotFoundException : Exception
{
    public CustomerNotFoundException(Guid id)
        : base($"Customer with id { id } not found!") 
    { }
}