namespace SimpleAPI.Application.Exceptions;

public class AllCustomerNotFoundException: Exception
{
    public AllCustomerNotFoundException() : base($"No one customer found!")
    {
    }
}