using MediatR;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Entities;

namespace SimpleAPI.Application.Entities.Note.Queries.GetAllNote;

/// <summary>
/// Get All Note Query
/// </summary>
public record GetAllNoteQuery(FilterSettings settings)  : IRequest<List<NoteResponse>>;