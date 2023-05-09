using MediatR;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;

namespace SimpleAPI.Application.Entities.Customer.Queries.GetAllCustomer;

/// <summary>
/// Get All Customer Query
/// </summary>
public record GetAllCustomerQuery(FilterSettings settings)  : IRequest<List<CustomerResponse>>;