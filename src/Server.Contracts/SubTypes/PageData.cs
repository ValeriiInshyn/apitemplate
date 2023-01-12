#region

using FluentValidation;

#endregion

namespace Server.Contracts.SubTypes;

public sealed record PageData(int Offset = 0, int Limit = 0);

/// <summary>
///     The page data validator class
/// </summary>
/// <seealso cref="AbstractValidator{T}" />
public sealed class PageDataValidator : AbstractValidator<PageData>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PageDataValidator" /> class
    /// </summary>
    public PageDataValidator()
    {
        RuleFor(d => d.Offset).GreaterThanOrEqualTo(0);
        RuleFor(d => d.Limit).GreaterThanOrEqualTo(0);
    }
}