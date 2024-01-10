using DTO.DTOs.PaymentDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.PaymentValidators;

public class PaymentServiceDTOValidator : BaseDTOValidator<PaymentServiceDTO>
{
    public PaymentServiceDTOValidator()
    {
        RuleFor(x => x.Name)
            //.NotNull().WithMessage("The Name is required and cannot be null.")
            .NotEmpty().WithMessage("The Name is required and cannot be empty.");
        RuleFor(x => x.Provider)
            //.NotNull().WithMessage("The Provider is required and cannot be null.")
            .NotEmpty().WithMessage("The Provider is required and cannot be empty.");
        RuleFor(x => x.Fee)
            .GreaterThanOrEqualTo(0).WithMessage("The Fee can not be negative.");
        RuleFor(x => x.FeePercentage)
            .GreaterThanOrEqualTo(0).WithMessage("The Fee Percentage can not be negative.");

        RuleForEach(x => x.PaymentDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new PaymentDTOValidator()));
    }
}
