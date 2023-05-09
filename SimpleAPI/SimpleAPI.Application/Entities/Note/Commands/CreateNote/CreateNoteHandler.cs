using MediatR;
using SimpleAPI.Application.Exceptions;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Note.Commands.CreateNote;

/// <summary>
/// Create Handler
/// </summary>
public sealed class CreateNoteHandler: IRequestHandler<CreateNoteCommand, NoteResponse>
{
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="noteRepository"></param>
    public CreateNoteHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    /// <summary>
    /// Create Note Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<NoteResponse> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Note()
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            Content = request.Content
        };
        
        var result = await _noteRepository.CreateAsync(entity);

        if (result is null)
        {
            throw new NoteNotCreatedException();
        }

        return new NoteResponse(
            result.Id,
            result.CustomerId, 
            result.Content,
            result.CreationDate,
            result.LastUpdateDate);
    }
}