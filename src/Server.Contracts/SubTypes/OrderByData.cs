#region

using FluentValidation;

#endregion

namespace Server.Contracts.SubTypes;

public sealed record OrderByData(string OrderBy, OrderDirection OrderDirection = OrderDirection.Desc);

/// <summary>
///     The order by data validator class
/// </summary>
/// <seealso cref="AbstractValidator{T}" />
public class OrderByDataValidator : AbstractValidator<OrderByData>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OrderByDataValidator" /> class
    /// </summary>
    public OrderByDataValidator()
    {
        RuleFor(d => d.OrderBy).NotEmpty();
        RuleFor(d => d.OrderDirection).IsInEnum();
    }
}