using MediatR;

namespace SimpleAPI.Application.Entities.Customer.Commands.DeleteCustomer;

/// <summary>
/// Delete Customer entity command
/// </summary>
/// <param name="Id"></param>
public sealed record DeleteCustomerCommand(Guid Id) : IRequest<int>
{
}