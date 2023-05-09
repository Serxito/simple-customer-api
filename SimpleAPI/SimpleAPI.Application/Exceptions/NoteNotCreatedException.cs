namespace SimpleAPI.Application.Exceptions;

public sealed class NoteNotCreatedException : Exception
{
    public NoteNotCreatedException() : base($"Failed to UPDATE Note entity!")
    {
    }
}