namespace SimpleAPI.Application.Responses;

/// <summary>
/// Note response
/// </summary>
/// <param name="CustomerId"></param>
/// <param name="Content"></param>
public sealed record NoteResponse(
    Guid Id, 
    Guid CustomerId, 
    string Content,
    DateTime CreationDate,
    DateTime LastUpdateDate)
{
}