using Business.Helpers.Validators.FinancialAidValidators;
using Business.Helpers.Validators.PaymentValidators;
using Business.Helpers.Validators.SharedValidators;
using Business.Helpers.Validators.UserValidators;
using DTO.DTOs.OrderDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.OrderValidators;

public class OrderDTOValidator : BaseDTOValidator<OrderDTO>
{
    public OrderDTOValidator()
    {
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("The Balance can not be negative.");
        RuleFor(x => x.Status).IsInEnum();

        RuleFor(x => x.CustomerId)
            .NotNull().WithMessage("The Customer Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Customer Id is required and cannot be empty.");
        RuleFor(x => x.CustomerDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new CustomerDTOValidator()));
        
        RuleFor(x => x.Establishment_ProductId)
            .NotNull().WithMessage("The Establishment Product Relationship Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Establishment Product Relationship Id is required and cannot be empty.");
        RuleFor(x => x.Establishment_ProductDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new Establishment_ProductDTOValidator()));

        RuleFor(x => x.PaymentDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new PaymentDTOValidator()));
        RuleFor(x => x.FinancialAidDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new FinancialAidDTOValidator()));
    }
}
