using SimpleAPI.Domain.Enums;

namespace SimpleAPI.Domain.Entities;

/// <summary>
/// Settings for sorting/paging
/// </summary>
/// <param name="order"></param>
public sealed record FilterSettings(PageSize limit, PageSize offset, Order order);