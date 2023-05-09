using MediatR;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Enums;

namespace SimpleAPI.Application.Entities.Customer.Queries.GetCustomerById;

/// <summary>
/// Get Customer By Id Query
/// </summary>
/// <param name="Id"></param>
public record GetCustomerByIdQuery(Guid Id)  : IRequest<CustomerResponse>;