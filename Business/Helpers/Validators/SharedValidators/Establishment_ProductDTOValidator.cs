using Business.Helpers.Validators.EstablishmentValidators;
using Business.Helpers.Validators.OrderValidators;
using Business.Helpers.Validators.ProductValidators;
using DTO.DTOs.SharedDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.SharedValidators;

public class Establishment_ProductDTOValidator : BaseDTOValidator<Establishment_ProductDTO>
{
    public Establishment_ProductDTOValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("The Quantity can not be negative.");
        RuleFor(x => x.PurchasePrice)
            .GreaterThanOrEqualTo(0).WithMessage("The Quantity can not be negative.");
        RuleFor(x => x.RetailPrice)
            .GreaterThanOrEqualTo(0).WithMessage("The Quantity can not be negative.");
        RuleFor(x => x.AidAmount)
            .GreaterThanOrEqualTo(0).WithMessage("The Aid Amount can not be negative.");
        RuleFor(x => x.AidPercentage)
            .GreaterThanOrEqualTo(0).WithMessage("The Aid Percentage can not be negative.");

        RuleFor(x => x.ExpirationDate)
            .NotNull().WithMessage("The Provider is required and cannot be null.")
            .NotEmpty().WithMessage("The Provider is required and cannot be empty.");


        RuleFor(x => x.ProductId)
            .NotNull().WithMessage("The Product Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Product Id is required and cannot be empty.");
        RuleFor(x => x.ProductDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new ProductDTOValidator()));

        RuleFor(x => x.EstablishmentId)
            .NotNull().WithMessage("The Establishment Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Establishment Id is required and cannot be empty.");
        RuleFor(x => x.EstablishmentDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new EstablishmentDTOValidator()));

        RuleForEach(x => x.OrderDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new OrderDTOValidator()));
    }
}
