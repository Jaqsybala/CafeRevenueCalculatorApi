using CafeRevenueCalculatorApi.Models;
using FluentValidation;

namespace CafeRevenueCalculatorApi.Validators
{
    public class RevenueCalculatorRequestValidator : AbstractValidator<RevenueCalculatorRequest>
    {
        public RevenueCalculatorRequestValidator()
        {
            RuleFor(x => x.TotalRevenue)
                .GreaterThan(0)
                .WithMessage("Total revenue must be greater than 0")
                .WithSeverity(Severity.Warning);
            RuleFor(x => x.RevenueFromKaspi)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Revenue from Kaspi must be greater than or equal to 0")
                .WithSeverity(Severity.Warning);
            RuleFor(x => x.RevenueFromOtherSources)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Revenue from other sources must be greater than or equal to 0")
                .WithSeverity(Severity.Warning);
            RuleFor(x => x.RevenueFromCash)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Revenue from cash must be greater than or equal to 0")
                .WithSeverity(Severity.Warning);
        }
    }
}