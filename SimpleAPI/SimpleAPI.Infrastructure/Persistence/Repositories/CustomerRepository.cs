using Dapper;
using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Infrastructure.Persistence.Repositories;

/// <summary>
/// Customer repository
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly DbContext _context;
    
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="context"></param>
    public CustomerRepository(DbContext context) => _context = context;

    /// <summary>
    /// Get all Customers
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Customer>> GetAllAsync(FilterSettings settings)
    {
        using var connection = _context.CreateConnection();

        var orderDirection = settings.order == Order.ASC ? "ASC" : "DESC";

        var query = $"\n" +
                    $" SELECT * \n" +
                    $" FROM Customers\n " +
                    $" ORDER BY Id { orderDirection }\n" +
                    $" LIMIT { (int)settings.limit }, { (int)settings.offset }\n";
        
        return await connection.QueryAsync<Customer>(query);
    }

    /// <summary>
    /// Get Customer by Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<Customer> GetByIdAsync(Guid Id)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            id = Id
        };
        
        var query = @"SELECT * FROM Customers WHERE Id = @id";
        
        return await connection.QuerySingleOrDefaultAsync<Customer>(query, parameters);
    }

    /// <summary>
    /// Create Customer entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Customer> CreateAsync(Customer entity)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            id = entity.Id,
            lastName = entity.LastName,
            firstName = entity.FirstName, 
            email = entity.Email,
            phone = entity.Phone,
            status = entity.Status
        };
        
        var query = @"
            INSERT INTO Customers(Id, FirstName, LastName, Email, Phone, Status)
            VALUES(@id, @firstName, @lastName, @email, @phone, @status)
            RETURNING *
        ";
        
        return await connection.QuerySingleOrDefaultAsync<Customer>(query, parameters);
    }

    /// <summary>
    /// Update Customer entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Customer> UpdateAsync(Customer entity)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            id = entity.Id,
            lastName = entity.LastName,
            firstName = entity.FirstName, 
            email = entity.Email,
            phone = entity.Phone,
            status = entity.Status
        };

        var query = @"
            UPDATE Customers
            SET 
                FirstName = @firstName,
                LastName = @lastName,
                Email = @email,
                Phone = @phone,
                Status = @status
            WHERE Id = @id
            RETURNING *
        ";
        
        return await connection.QuerySingleOrDefaultAsync<Customer>(query, parameters);
    }

    /// <summary>
    /// Delete Customer by Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<int> DeleteAsync(Guid Id)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            id = Id
        };
        
        var query = @"
            DELETE 
            FROM Customers
            WHERE Id = @id";
        
        return await connection.ExecuteAsync(query, parameters);
    }
}