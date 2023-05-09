using MediatR;
using SimpleAPI.Application.Exceptions;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Customer.Commands.CreateCustomer;

/// <summary>
/// Create Handler
/// </summary>
public sealed class CreateCustomerHandler: IRequestHandler<CreateCustomerCommand, CustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="customerRepository"></param>
    public CreateCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    /// <summary>
    /// Create Customer Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Customer()
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Status = request.Status
        };
        
        var result = await _customerRepository.CreateAsync(entity);

        if (result is null)
        {
            throw new CustomerNotCreatedException();
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