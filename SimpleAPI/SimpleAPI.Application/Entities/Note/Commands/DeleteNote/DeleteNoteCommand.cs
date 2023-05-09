using MediatR;

namespace SimpleAPI.Application.Entities.Note.Commands.DeleteNote;

/// <summary>
/// Delete Note entity command
/// </summary>
/// <param name="Id"></param>
public sealed record DeleteNoteCommand(Guid Id) : IRequest<int>
{
}