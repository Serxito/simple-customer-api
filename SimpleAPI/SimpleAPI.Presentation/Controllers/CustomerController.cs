using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Application.Entities.Customer.Commands.CreateCustomer;
using SimpleAPI.Application.Entities.Customer.Commands.UpdateCustomer;
using SimpleAPI.Application.Entities.Customer.Queries.GetAllCustomer;
using SimpleAPI.Application.Entities.Customer.Queries.GetCustomerById;
using SimpleAPI.Application.Entities.Note.Commands.DeleteNote;
using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;
using SimpleAPI.Presentation.Abstractions;

namespace SimpleAPI.Presentation.Controllers;

/// <summary>
/// Customer controller
/// </summary>
[Route("api/customer")]
[AllowAnonymous]
public class CustomerController : ApiController
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="sender"></param>
    public CustomerController(ISender sender) : base(sender)
    {
    }
    
    /// <summary>
    /// Get Customer by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("id")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
    
    /// <summary>
    /// Get all Customers
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllCustomersAsync(
        int limit,
        int offset,
        int order,
        CancellationToken cancellationToken)
    {
        var query = new GetAllCustomerQuery(new FilterSettings((PageSize)limit, (PageSize)offset, (Order)order));
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Create new Customer entity
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="status"></param>
    /// <param name="email"></param>
    /// <param name="phone"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateCustomerAsync(
        string firstName, 
        string lastName, 
        int status, 
        string email, 
        string phone, 
        CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand(
            firstName, 
            lastName, 
            (Status)status, 
             email, 
             phone);
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Update existing Customer entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="status"></param>
    /// <param name="email"></param>
    /// <param name="phone"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateCustomerAsync(
        Guid id,
        string firstName, 
        string lastName, 
        int status, 
        string email, 
        string phone, 
        CancellationToken cancellationToken)
    {
        var command = new UpdateCustomerCommand(
            id,
            firstName, 
            lastName, 
            (Status)status, 
            email, 
            phone);
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Delete existing Customer by id
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