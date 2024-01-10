using Business.Helpers.Validators.SharedValidators;
using DTO.DTOs.ProductDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.ProductValidators;

public class ProductImageDTOValidator : BaseImageDTOValidator<ProductImageDTO>
{
    public ProductImageDTOValidator()
    {
        RuleFor(x => x.ProductId)
            .NotNull().WithMessage("The Product Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Product Id is required and cannot be empty.");
        RuleFor(x => x.ProductDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new ProductDTOValidator()));
    }
}
