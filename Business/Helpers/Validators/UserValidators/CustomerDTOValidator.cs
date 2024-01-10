using Business.Helpers.Validators.OrderValidators;
using DTO.DTOs.UserDTOs.CustomerDTOs;
using FluentValidation;
using Common.Extensions;

namespace Business.Helpers.Validators.UserValidators;

public class CustomerDTOValidator : ApplicationUserDTOValidator<CustomerDTO>
{
    public CustomerDTOValidator()
    {
        RuleFor(x => x.Address)
            .NotNull().WithMessage("The Address is required and cannot be null.")
            .NotEmpty().WithMessage("The Address is required and cannot be empty.");

        RuleForEach(x => x.Orders).CustomAsync(async (p, cx, ct) => await (p, cx, ct).HandelNullableEntityAsync(new OrderDTOValidator()));
    }
}
