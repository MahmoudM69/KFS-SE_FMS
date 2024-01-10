using DTO.DTOs.UserDTOs;
using FluentValidation;

namespace Business.Helpers.Validators.UserValidators;

public class ApplicationUserDTOValidator<Tuser> : BaseDTOValidator<Tuser> where Tuser : ApplicationUserDTO, new()
{
    public ApplicationUserDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("The UserName is required and cannot be null.")
            .NotEmpty().WithMessage("The UserName is required and cannot be empty.");
        RuleFor(x => x.Email)
            .NotNull().WithMessage("The Email is required and cannot be null.")
            .NotEmpty().WithMessage("The Email is required and cannot be empty.")
            .EmailAddress().WithMessage("The Email is not valid.");
        RuleFor(x => x.DOB)
            .NotNull().WithMessage("The Date Of Birth is required and cannot be null.")
            .NotEmpty().WithMessage("The Date Of Birth is required and cannot be empty.");

        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("The Balance can not be negative.");
        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(0).WithMessage("The Salary can not be negative.");
    }
}
