namespace SimpleAPI.Application.Exceptions;

/// <inheritdoc />
public sealed class NoteNotFoundException : Exception
{
    public NoteNotFoundException(Guid id) : base($"Note with id { id } not found!")
    {
    }
}