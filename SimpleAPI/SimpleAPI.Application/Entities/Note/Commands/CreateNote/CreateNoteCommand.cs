using MediatR;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;

namespace SimpleAPI.Application.Entities.Note.Commands.CreateNote;

/// <summary>
/// Create Note entity command
/// </summary>
/// <param name="CustomerId"></param>
/// <param name="Content"></param>
public sealed record CreateNoteCommand(Guid CustomerId, string Content) : IRequest<NoteResponse>
{
}