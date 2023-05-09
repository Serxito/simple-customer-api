using System.Data;
using System.Data.SQLite;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SimpleAPI.Infrastructure.Persistence;

public class DbContext
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="configuration"></param>
    public DbContext(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Create DB connection
    /// </summary>
    /// <returns></returns>
    public IDbConnection CreateConnection()
        => new SQLiteConnection(GetDataBaseDirectory());
    
    /// <summary>
    /// Create DB and clear on startup
    /// </summary>
    public async Task InitDataBase()
    {
        try
        {
            using var connection = CreateConnection();
            
            await _CleanDb();
            await _InitCustomers();
            await _InitNotes();
            
            #region SQL Commands
            
            async Task _CleanDb()
            {
                var command = @"
                DROP TABLE IF EXISTS Customers;
                DROP TABLE IF EXISTS Notes;
            ";
             
                await connection.ExecuteAsync(command);
            }

            async Task _InitCustomers()
            {
                var command = @"
                CREATE TABLE IF NOT EXISTS 
                Customers (
                    Id UUID BLOB NOT NULL PRIMARY KEY,
                    FirstName NVARCHAR(20) NOT NULL,
                    LastName NVARCHAR(20) NOT NULL,
                    Email TEXT UNIQUE NOT NULL,
                    Status INTEGER NOT NULL,
                    Phone TEXT NULL,
                    CreationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastUpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP
                );
            ";
            
                await connection.ExecuteAsync(command);
            }
            
            async Task _InitNotes()
            {
                var command = @"
                CREATE TABLE IF NOT EXISTS 
                Notes (
                    Id UUID BLOB NOT NULL PRIMARY KEY,
                    CustomerId UUID BLOB NOT NULL,
                    Content TEXT NOT NULL,
                    CreationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastUpdateDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (CustomerId)  REFERENCES Customers (Id)
                );
            ";
            
                await connection.ExecuteAsync(command);
            }
            
            #endregion
        }
        catch (Exception ex)
        {
            ////TODO: CREATE SPECIFIC EXCEPTION FOR THIS EVENT
            Console.WriteLine(ex);
        }
    }
    
    private string GetDataBaseDirectory()
    {
        var assesmblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        var rootDirectory = Directory
            .GetParent(Path.GetDirectoryName(assesmblyPath))
            .Parent
            .Parent
            .Parent
            .FullName;
        
        var projectsDirectories = Directory.GetDirectories(rootDirectory)
            .FirstOrDefault(x=>x.Contains("Infrastructure"));
        
        var dbDirectory = Directory.GetDirectories(
                projectsDirectories, "*.*", SearchOption.AllDirectories)
            .FirstOrDefault(x=>x.Contains("DataBase")) + "\\";

            var dbLocation = _configuration.GetConnectionString("SimpleApiDBConnection")
            .Replace("@", dbDirectory);

        return dbLocation;
    }
}