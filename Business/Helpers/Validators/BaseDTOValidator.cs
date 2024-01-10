using FluentValidation;
using Common.Interfaces;

namespace Business.Helpers.Validators;

public class BaseDTOValidator<T> : AbstractValidator<T> where T : IValidatable, new()
{
    public BaseDTOValidator()
    { }
}
