using Business.Helpers.Validators.SharedValidators;
using DTO.DTOs.ProductDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.ProductValidators;

public class ProductTypeDTOValidator : BaseTypeDTOValidator<ProductTypeDTO>
{
    public ProductTypeDTOValidator()
    {
        RuleForEach(x => x.ProductDTOs)
            .CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new ProductDTOValidator()));
        RuleForEach(x => x.ProductType_FinancialAidDTOs)
            .CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new ProductType_FinancialAidDTOValidator()));
    }
}
