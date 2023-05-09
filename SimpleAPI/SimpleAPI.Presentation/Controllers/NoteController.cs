using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Application.Entities.Note.Commands.CreateNote;
using SimpleAPI.Application.Entities.Note.Commands.DeleteNote;
using SimpleAPI.Application.Entities.Note.Commands.UpdateNote;
using SimpleAPI.Application.Entities.Note.Queries.GetAllNote;
using SimpleAPI.Application.Entities.Note.Queries.GetNoteById;
using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;
using SimpleAPI.Presentation.Abstractions;

namespace SimpleAPI.Presentation.Controllers;

/// <summary>
/// Note controller
/// </summary>
[Route("api/note")]
[AllowAnonymous]
public class NoteController : ApiController
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="sender"></param>
    public NoteController(ISender sender) : base(sender)
    {
    }
    
    /// <summary>
    /// Get Note by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("id")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetNoteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetNoteByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
    
    /// <summary>
    /// Get all Notes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllNotesAsync(
        int limit,
        int offset,
        int order,
        CancellationToken cancellationToken)
    {
        var query = new GetAllNoteQuery(new FilterSettings((PageSize)limit, (PageSize)offset, (Order)order));
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Create new Note entity
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateNoteAsync(Guid customerId, string content, CancellationToken cancellationToken)
    {
        var command = new CreateNoteCommand(customerId, content);
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Update existing Note entity
    /// </summary>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateNoteAsync(
        Guid id, 
        Guid customerId, 
        string content, 
        CancellationToken cancellationToken)
    {
        var command = new UpdateNoteCommand(id, customerId,  content);
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Delete existing Note by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("id")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteNoteAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteNoteCommand(id);
        var result = await Sender.Send(command, cancellationToken);

        return result == 1 ? Ok() : BadRequest();
    }
}