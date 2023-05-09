using MediatR;
using SimpleAPI.Application.Exceptions;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Note.Commands.DeleteNote;

public sealed class DeleteNoteHandler: IRequestHandler<DeleteNoteCommand, int>
{
    private readonly INoteRepository _noteRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="noteRepository"></param>
    public DeleteNoteHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    /// <summary>
    /// Delete Note Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
       var result = await _noteRepository.DeleteAsync(request.Id);
        
        if (result == 0)
        {
            throw new NoteNotFoundException(request.Id);
        }

        return result;
    }
}