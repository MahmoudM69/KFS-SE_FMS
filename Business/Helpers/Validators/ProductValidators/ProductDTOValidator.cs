using Business.Helpers.Validators.SharedValidators;
using DTO.DTOs.ProductDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.ProductValidators;

public class ProductDTOValidator : BaseDTOValidator<ProductDTO>
{
    public ProductDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("The Name is required and cannot be null.")
            .NotEmpty().WithMessage("The Name is required and cannot be empty.");

        RuleFor(x => x.ProductTypeDTOs)
            .NotNull().WithMessage("The Product Type List is required and cannot be null.")
            .NotEmpty().WithMessage("The Product Type List is required and cannot be empty.");
        RuleForEach(x => x.ProductTypeDTOs)
            .NotNull().WithMessage("The Product Type is required and cannot be null.")
            .NotEmpty().WithMessage("The Product Type is required and cannot be empty.")
            .CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new ProductTypeDTOValidator(), false));

        RuleForEach(x => x.ProductImageDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new ProductImageDTOValidator()));
        RuleForEach(x => x.Establishment_ProductDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new Establishment_ProductDTOValidator()));
    }
}
