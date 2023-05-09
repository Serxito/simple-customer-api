using MediatR;
using SimpleAPI.Application.Responses;

namespace SimpleAPI.Application.Entities.Note.Queries.GetNoteById;

/// <summary>
/// Get Note By Id Query
/// </summary>
/// <param name="Id"></param>
public record GetNoteByIdQuery(Guid Id)  : IRequest<NoteResponse>;