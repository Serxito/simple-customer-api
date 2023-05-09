using MediatR;
using SimpleAPI.Application.Entities.Note.Commands.UpdateNote;
using SimpleAPI.Application.Exceptions;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Note.Commands.UpdateNote;

/// <summary>
/// Create Handler
/// </summary>
public sealed class UpdateNoteHandler: IRequestHandler<UpdateNoteCommand, NoteResponse>
{
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="noteRepository"></param>
    public UpdateNoteHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    /// <summary>
    /// Update Note Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<NoteResponse> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Note()
        {
            Id = request.Id,
            CustomerId = request.CustomerId,
            Content = request.Content
        };
        
        var result = await _noteRepository.UpdateAsync(entity);

        if (result is null)
        {
            throw new NoteNotUpdatedException();
        }

        return new NoteResponse(
            result.Id,
            result.CustomerId, 
            result.Content,
            result.CreationDate,
            result.LastUpdateDate);
    }
}