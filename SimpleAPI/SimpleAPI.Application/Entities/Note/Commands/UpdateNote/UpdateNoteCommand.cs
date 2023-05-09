using MediatR;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;

namespace SimpleAPI.Application.Entities.Note.Commands.UpdateNote;

/// <summary>
/// Update Note entity command
/// </summary>
/// <param name="Id"></param>
/// <param name="CustomerId"></param>
/// <param name="Content"></param>
public sealed record UpdateNoteCommand(Guid Id, Guid CustomerId, string Content) : IRequest<NoteResponse>
{
}