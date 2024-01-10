using Business.Helpers.Validators.OrderValidators;
using DTO.DTOs.PaymentDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.PaymentValidators;

public class PaymentDTOValidator : BaseDTOValidator<PaymentDTO>
{
    public PaymentDTOValidator()
    {
        RuleFor(x => x.Date)
            .NotNull().WithMessage("The Date is required and cannot be null.")
            .NotEmpty().WithMessage("The Date is required and cannot be empty.");

        RuleFor(x => x.Status).IsInEnum();

        RuleFor(x => x.PaymentServiceId)
            .NotNull().WithMessage("The Payment Service Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Payment Service Id is required and cannot be empty.");
        RuleFor(x => x.PaymentServiceDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new PaymentServiceDTOValidator()));

        RuleForEach(x => x.OrderDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new OrderDTOValidator()));
    }
}
