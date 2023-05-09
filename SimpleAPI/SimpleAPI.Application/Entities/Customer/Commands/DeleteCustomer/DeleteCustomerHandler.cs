using MediatR;
using SimpleAPI.Domain.Exceptions;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Customer.Commands.DeleteCustomer;

public sealed class DeleteCustomerHandler: IRequestHandler<DeleteCustomerCommand, int>
{
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="customerRepository"></param>
    public DeleteCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    /// <summary>
    /// Delete Customer Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
       var result = await _customerRepository.DeleteAsync(request.Id);

       if (result == 0)
       {
           throw new CustomerNotFoundException(request.Id);
       }

       return result;
    }
}