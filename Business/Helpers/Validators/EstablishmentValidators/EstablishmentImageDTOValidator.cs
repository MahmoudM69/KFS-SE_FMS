using Business.Helpers.Validators.SharedValidators;
using DTO.DTOs.EstablishmentDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.EstablishmentValidators;

public class EstablishmentImageDTOValidator : BaseImageDTOValidator<EstablishmentImageDTO>
{
    public EstablishmentImageDTOValidator()
    {
        RuleFor(x => x.EstablishmentId)
            .NotNull().WithMessage("The Establishment Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Establishment Id is required and cannot be empty.");
        RuleFor(x => x.EstablishmentDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new EstablishmentDTOValidator()));
    }
}
