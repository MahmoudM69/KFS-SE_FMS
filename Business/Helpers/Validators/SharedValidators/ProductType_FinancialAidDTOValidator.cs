using Business.Helpers.Validators.FinancialAidValidators;
using Business.Helpers.Validators.ProductValidators;
using DTO.DTOs.SharedDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.SharedValidators;

public class ProductType_FinancialAidDTOValidator : BaseDTOValidator<ProductType_FinancialAidDTO>
{
    public ProductType_FinancialAidDTOValidator()
    {
        RuleFor(x => x.ProductTypeId)
            .NotNull().WithMessage("The Product Type Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Product Type Id is required and cannot be empty.");
        RuleFor(x => x.ProductTypeDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new ProductTypeDTOValidator()));

        RuleFor(x => x.FinancialAidId)
            .NotNull().WithMessage("The Financial Aid Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Financial Aid Id is required and cannot be empty.");
        RuleFor(x => x.FinancialAidDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new FinancialAidDTOValidator()));
    }
}
