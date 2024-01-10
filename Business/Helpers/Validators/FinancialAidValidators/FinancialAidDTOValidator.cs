using Business.Helpers.Validators.EstablishmentValidators;
using Business.Helpers.Validators.OrderValidators;
using Business.Helpers.Validators.SharedValidators;
using DTO.DTOs.FinancialAidDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.FinancialAidValidators;

public class FinancialAidDTOValidator : BaseDTOValidator<FinancialAidDTO>
{
    public FinancialAidDTOValidator()
    {
        RuleFor(x => x.MinBalance)
            .GreaterThanOrEqualTo(0).WithMessage("The Min Balance can not be negative.");
        RuleFor(x => x.MaxBalance)
            .GreaterThanOrEqualTo(0).WithMessage("The Max Balance can not be negative.");
        RuleFor(x => x.AidAmount)
            .GreaterThanOrEqualTo(0).WithMessage("The Aid Amount can not be negative.");
        RuleFor(x => x.AidPercentage)
            .GreaterThanOrEqualTo(0).WithMessage("The Aid Percentage can not be negative.");
        RuleFor(x => x.Budget)
            .GreaterThanOrEqualTo(0).WithMessage("The Budget can not be negative.");

        RuleFor(x => x.EstablishmentId)
            .NotNull().WithMessage("The Establishment Id Url was not found.")
            .NotEmpty().WithMessage("The Establishment Id Url was not found.");
        RuleFor(x => x.EstablishmentDTO)
            .CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new EstablishmentDTOValidator()));

        RuleForEach(x => x.OrderDTOs)
            .CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new OrderDTOValidator()));
        RuleForEach(x => x.ProductType_FinancialAidDTOs)
            .CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new ProductType_FinancialAidDTOValidator()));
    }
}
