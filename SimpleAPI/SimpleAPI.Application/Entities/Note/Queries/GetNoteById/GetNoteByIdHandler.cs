using MediatR;
using SimpleAPI.Application.Exceptions;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Exceptions;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Note.Queries.GetNoteById;

public sealed class GetNoteByIdHandler: IRequestHandler<GetNoteByIdQuery, NoteResponse>
{
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="noteRepository"></param>
    public GetNoteByIdHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    /// <summary>
    /// Get Note By Id Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<NoteResponse> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _noteRepository.GetByIdAsync(request.Id);

        if (result is null)
        {
            throw new AllNotesNotFoundException();
        }

        return new NoteResponse(
            result.Id,
            result.CustomerId, 
            result.Content,
            result.CreationDate,
            result.LastUpdateDate);
    }
}