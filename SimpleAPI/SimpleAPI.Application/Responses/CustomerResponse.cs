using SimpleAPI.Domain.Entities;
using SimpleAPI.Domain.Enums;

namespace SimpleAPI.Application.Responses;

/// <summary>
/// Customer response
/// </summary>
/// <param name="Id"></param>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Status"></param>
/// <param name="Email"></param>
/// <param name="Phone"></param>
/// <param name="CreationDate"></param>
/// <param name="LastUpdateDate"></param>
public sealed record CustomerResponse(
    Guid Id,
    string FirstName, 
    string LastName, 
    Status Status, 
    string Email, 
    string Phone,
    DateTime CreationDate,
    DateTime LastUpdateDate);