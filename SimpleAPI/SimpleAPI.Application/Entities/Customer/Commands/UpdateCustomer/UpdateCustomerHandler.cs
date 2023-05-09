using MediatR;
using SimpleAPI.Application.Exceptions;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Customer.Commands.UpdateCustomer;

public sealed class UpdateCustomerHandler: IRequestHandler<UpdateCustomerCommand, CustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="customerRepository"></param>
    public UpdateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    /// <summary>
    /// Update Customer Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Customer()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Status = request.Status
        };
        
        var result = await _customerRepository.UpdateAsync(entity);

        if (result is null)
        {
            throw new CustomerNotUpdatedException();
        }

        var response = new CustomerResponse(
            result.Id,
            result.FirstName, 
            result.LastName, 
            result.Status, 
            result.Email,
            result.Phone,
            result.CreationDate,
            result.LastUpdateDate);

        return response;
    }
}