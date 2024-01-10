using DTO.DTOs.SharedDTOs;
using FluentValidation;

namespace Business.Helpers.Validators.SharedValidators;

public class BaseTypeDTOValidator<T> : BaseDTOValidator<T> where T : BaseTypeDTO, new()
{
    public BaseTypeDTOValidator()
    {
        RuleFor(x => x.Type)
            .NotNull().WithMessage("The Type is required and cannot be null.")
            .NotEmpty().WithMessage("The Type is required and cannot be empty.");
    }
}
