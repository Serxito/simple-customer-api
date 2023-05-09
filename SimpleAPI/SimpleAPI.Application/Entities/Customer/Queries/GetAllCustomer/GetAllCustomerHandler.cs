using MediatR;
using SimpleAPI.Application.Exceptions;
using SimpleAPI.Application.Responses;
using SimpleAPI.Domain.Exceptions;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Application.Entities.Customer.Queries.GetAllCustomer;

public sealed class GetAllCustomerHandler: IRequestHandler<GetAllCustomerQuery, List<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="customerRepository"></param>
    public GetAllCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    /// <summary>
    /// Get Customer Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<CustomerResponse>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var result = new List<CustomerResponse>();
        var entities = await _customerRepository.GetAllAsync(request.settings);

        if (result is null)
        {
            throw new AllCustomerNotFoundException();
        }

        result.AddRange(entities.Select(entity => 
            new CustomerResponse(
                entity.Id,
                entity.FirstName, 
                entity.LastName, 
                entity.Status, 
                entity.Email, 
                entity.Phone,
                entity.CreationDate,
                entity.LastUpdateDate)));

        return result;
    }
}