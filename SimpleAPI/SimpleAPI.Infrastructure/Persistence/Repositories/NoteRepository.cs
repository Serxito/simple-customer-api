using Dapper;
using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;
using SimpleAPI.Domain.Repositories;

namespace SimpleAPI.Infrastructure.Persistence.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly DbContext _context;
    
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="context"></param>
    public NoteRepository(DbContext context) => _context = context;
    
    /// <summary>
    /// Get all Notes entities
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Note>> GetAllAsync(FilterSettings settings)
    {
         using var connection = _context.CreateConnection();
         
         var orderDirection = settings.order == Order.ASC ? "ASC" : "DESC";

         var query = $"\n" +
                     $" SELECT * \n" +
                     $" FROM Notes\n " +
                     $" ORDER BY Id { orderDirection }\n" +
                     $" LIMIT { (int)settings.limit }, { (int)settings.offset }\n";
         
         return await connection.QueryAsync<Note>(query);
    }

    /// <summary>
    /// Get Note by Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<Note> GetByIdAsync(Guid Id)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            id = Id
        };
        
        var query = @"
            SELECT * 
            FROM Notes
            WHERE Id = @id
        ";
        
        return await connection.QuerySingleOrDefaultAsync<Note>(query, parameters);
    }

    /// <summary>
    /// Create Note entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Note> CreateAsync(Note entity)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            id = entity.Id,
            content = entity.Content,
            customerId = entity.CustomerId
        };
        
        var query = @"
            INSERT INTO Notes(Id, Content, CustomerId)
            VALUES(@id, @content, @customerId)
            RETURNING *
        ";
        
        return await connection.QuerySingleOrDefaultAsync<Note>(query, parameters);
    }

    /// <summary>
    /// Update Note entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<Note> UpdateAsync(Note entity)
    {
        using var connection = _context.CreateConnection();
        
        var parameters = new
        {
            id = entity.Id,
            content = entity.Content,
            customerId = entity.CustomerId
        };
        
        var query = @"
            UPDATE Notes
            SET 
                Content = @content,
                CustomerId = @customerId
            WHERE Id = @id
            RETURNING *
        ";
        
        return await connection.QuerySingleOrDefaultAsync<Note>(query, parameters);
    }

    /// <summary>
    /// Delete Note by Id
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
            FROM Notes
            WHERE Id = @id";
        
        return await connection.ExecuteAsync(query, parameters);
    }
}