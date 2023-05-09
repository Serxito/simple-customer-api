namespace SimpleAPI.Domain.Entities;

/// <summary>
/// Note entity
/// </summary>
public class Note : BaseEntity
{
    /// <summary>
    /// Customer identification
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Content
    /// </summary>
    public string Content { get; set; }
}