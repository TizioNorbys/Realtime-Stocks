using FluentValidation;
using Stocks.Application.DTOs.Stocks;

namespace Stocks.Application.Validators.Stocks;

public class HistoricalDataValidator : AbstractValidator<HistoricalDataDto>
{
    public HistoricalDataValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.MinValue);

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .Must((x, endDate) => endDate > x.StartDate)
                .WithMessage("End date must be higher than start date");
    }
}