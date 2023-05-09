using MediatR;
using SimpleAPI.Application.Exceptions;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Exceptions;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Note.Queries.GetAllNote;

public sealed class GetAllNoteHandler: IRequestHandler<GetAllNoteQuery, List<NoteResponse>>
{
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="noteRepository"></param>
    public GetAllNoteHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    /// <summary>
    /// Get All Note Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<NoteResponse>> Handle(GetAllNoteQuery request, CancellationToken cancellationToken)
    {
        var entities = await _noteRepository.GetAllAsync(request.settings);

        if (entities is null)
        {
            throw new AllNotesNotFoundException();
        }

        return entities.Select(entity => new NoteResponse(
            entity.Id,
            entity.CustomerId, 
            entity.Content,
            entity.CreationDate,
            entity.LastUpdateDate)).ToList();
    }
}