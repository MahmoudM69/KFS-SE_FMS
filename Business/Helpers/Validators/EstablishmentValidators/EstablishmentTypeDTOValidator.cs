using Business.Helpers.Validators.SharedValidators;
using DTO.DTOs.EstablishmentDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.EstablishmentValidators;

public class EstablishmentTypeDTOValidator : BaseTypeDTOValidator<EstablishmentTypeDTO>
{
    public EstablishmentTypeDTOValidator()
    {
        RuleForEach(x => x.EstablishmentDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new EstablishmentDTOValidator()));
    }
}
