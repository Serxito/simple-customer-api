namespace SimpleAPI.Application.Exceptions;

public class NoteNotUpdatedException : Exception
{
    public NoteNotUpdatedException() : base($"Failed to UPDATE Note entity!")
    {
    }
}