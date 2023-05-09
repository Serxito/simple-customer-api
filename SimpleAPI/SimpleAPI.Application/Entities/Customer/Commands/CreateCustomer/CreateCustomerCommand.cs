using MediatR;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;

namespace SimpleAPI.Application.Entities.Customer.Commands.CreateCustomer;

/// <summary>
/// Create Customer entity command
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Status"></param>
/// <param name="Email"></param>
/// <param name="Phone"></param>
public sealed record CreateCustomerCommand(
    string FirstName, 
    string LastName, 
    Status Status, 
    string Email, 
    string Phone) : IRequest<CustomerResponse>
{
}