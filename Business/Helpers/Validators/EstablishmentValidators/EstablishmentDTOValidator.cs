using Business.Helpers.Validators.FinancialAidValidators;
using Business.Helpers.Validators.SharedValidators;
using Business.Helpers.Validators.UserValidators;
using DTO.DTOs.EstablishmentDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.EstablishmentValidators;

public class EstablishmentDTOValidator : BaseDTOValidator<EstablishmentDTO>
{
    public EstablishmentDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("The Name is required and cannot be null.")
            .NotEmpty().WithMessage("The Name is required and cannot be empty.");
        RuleFor(x => x.Address)
            .NotNull().WithMessage("The Address is required and cannot be null.")
            .NotEmpty().WithMessage("The Address is required and cannot be empty.");
        RuleFor(x => x.LogoUrl)
            .NotNull().WithMessage("The Logo Url was not found.")
            .NotEmpty().WithMessage("The Logo Url was not found.");
        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("The Balance can not be negative.");

        RuleFor(x => x.EstablishmentTypeDTOs)
            .NotNull().WithMessage("The Establishment Type List is required and cannot be null.")
            .NotEmpty().WithMessage("The Establishment Type List is required and cannot be empty.");
        RuleForEach(x => x.EstablishmentTypeDTOs)
            .NotNull().WithMessage("The Establishment Type is required and cannot be null.")
            .NotEmpty().WithMessage("The Establishment Type is required and cannot be empty.")
            .CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new EstablishmentTypeDTOValidator(), false));

        RuleForEach(x => x.EstablishmentImageDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new EstablishmentImageDTOValidator()));

        RuleForEach(x => x.EmployeeDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new EmployeeDTOValidator()));
        RuleForEach(x => x.FinancialAidDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new FinancialAidDTOValidator()));
        RuleForEach(x => x.Establishment_ProductDTOs).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new Establishment_ProductDTOValidator()));
    }
}
