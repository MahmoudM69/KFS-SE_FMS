using DTO.DTOs.SharedDTOs;
using FluentValidation;

namespace Business.Helpers.Validators.SharedValidators;

public class BaseImageDTOValidator<T> : BaseDTOValidator<T> where T : BaseImageDTO, new()
{
    public BaseImageDTOValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotNull().WithMessage("The Image Url was not found.")
            .NotEmpty().WithMessage("The Image Url was not found.");
    }
}
