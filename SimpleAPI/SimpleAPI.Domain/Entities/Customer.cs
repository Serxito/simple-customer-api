using SimpleAPI.Domain.Enums;

namespace SimpleAPI.Domain.Entities;

/// <summary>
/// Customer entity
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// FirstName
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// LastName
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Phone
    /// </summary>
    public string Phone { get; set; }
}