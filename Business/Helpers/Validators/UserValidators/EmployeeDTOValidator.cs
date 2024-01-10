using Business.Helpers.Validators.EstablishmentValidators;
using DTO.DTOs.UserDTOs.EmpolyeeDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.UserValidators;

public class EmployeeDTOValidator : ApplicationUserDTOValidator<EmployeeDTO>
{
    public EmployeeDTOValidator()
    {
        RuleFor(x => x.WorkingSince)
            .NotNull().WithMessage("The Working Since Date is required and cannot be null.")
            .NotEmpty().WithMessage("The Working Since Date is required and cannot be empty.");


        RuleFor(x => x.EstablishmentId)
            .NotNull().WithMessage("The Establishment Id is required and cannot be null.")
            .NotEmpty().WithMessage("The Establishment Id is required and cannot be empty.");
        RuleFor(x => x.EstablishmentDTO).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new EstablishmentDTOValidator()));
    }
}
