namespace SimpleAPI.Application.Exceptions;

public class AllNotesNotFoundException: Exception
{
    public AllNotesNotFoundException() : base($"No one note found!")
    {
    }
}