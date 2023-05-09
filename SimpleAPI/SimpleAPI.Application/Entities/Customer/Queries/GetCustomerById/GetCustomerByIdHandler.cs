using MediatR;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Exceptions;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Customer.Queries.GetCustomerById;

public sealed class GetCustomerByIdHandler: IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="customerRepository"></param>
    public GetCustomerByIdHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    /// <summary>
    /// Get Customer Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _customerRepository.GetByIdAsync(request.Id);

        if (result is null)
        {
            throw new CustomerNotFoundException(request.Id);
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