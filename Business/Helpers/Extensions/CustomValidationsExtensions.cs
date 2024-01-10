using Common.Exceptions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Common.Interfaces;
using System.Data.SqlTypes;
using FluentValidation;
using Common.Classes;
using FluentValidation.Results;
using Humanizer;
using DataAccess.Models.UserModels;
using DTO.DTOs.BaseDTOs;

namespace Business.Helpers.Extensions;

public static class CustomValidationsExtensions
{
    public static bool ValidateString(this string str)
    {
        if (str == null || string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str)) return false;
        return true;
    }

    public static (bool IsValid, string ErrorMessage) ValidateId<T>(this int id, bool isIdZero = false)
    {
        var type = typeof(T) is INullable ? "" : $"{typeof(T).Name}'s ";
        if (isIdZero && id != 0)
            return (false, $"The {type}Id can only be zero.");
        if (!isIdZero && id < 1)
            return (false, $"The {type}Id cannot be zero or negative.");
        return (true, "");
    }
    public static (bool IsValid, string ErrorMessage) ValidateId<T>(this string id, bool isIdEmpty = false)
    {
        var type = typeof(T) is INullable ? "" : $"{typeof(T).Name}'s ";
        if (isIdEmpty && id.ValidateString())
            return (false, $"The {type}Id cannot be null or empty.");
        else if (!isIdEmpty && !id.ValidateString())
            return (false, $"The {type}Id can only be empty.");
        return (true, "");
    }

    public static (bool IsValid, IEnumerable<string> ErrorMessageList) ValidateIds<T>(this IEnumerable<int> ids, bool isIdZero = false)
    {
        if (ids == null || !ids.Any()) return (false, new List<string>() { "No Ids were sent." });
        var isValid = true;
        var errorMessageList = new List<string>();
        foreach (var id in ids)
        {
            var (isIdValid, ErrorMessage) = id.ValidateId<T>(isIdZero);
            if (isValid && !isIdValid) isValid = false;
            errorMessageList.Add(ErrorMessage);
        }
        return (isValid, errorMessageList);
    }
    public static (bool IsValid, IEnumerable<string> ErrorMessageList) ValidateIds<T>(this IEnumerable<string> ids, bool isIdEmpty = false)
    {
        if (ids == null || !ids.Any()) return (false, new List<string>() { "No Ids were sent." });
        var isValid = true;
        var errorMessageList = new List<string>();
        foreach (var id in ids)
        {
            var (isIdValid, ErrorMessage) = id.ValidateId<T>(isIdEmpty);
            if (isValid && !isIdValid) isValid = false;
            errorMessageList.Add(ErrorMessage);
        }
        return (isValid, errorMessageList);
    }

    private static async Task<(bool IsValid, IEnumerable<string> Errors)> ValidateEntityAsync<T>
        (this T entity, IValidator<T> validator, bool? isIdEmpty = null, CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            if (entity == null)
                return (false, new List<string>() { $"No {typeof(T).Name} was sent." });
            if (validator == null)
                throw new ValidationNotImplementedException<T>($"No validators were registered for class \"{typeof(T).Name}\".");

            var errorMessages = new List<string>();
            if (isIdEmpty != null)
            {
                if (entity is ApplicationUser baseUser)
                {
                    var (IsValid, ErrorMessage) = baseUser.Id.ValidateId<T>(isIdEmpty.Value);
                    if (!IsValid) errorMessages.Add(ErrorMessage);
                }
                else if (entity is BaseEntity baseModel)
                {
                    var (IsValid, ErrorMessage) = baseModel.Id.ValidateId<T>(isIdEmpty.Value);
                    if (!IsValid) errorMessages.Add(ErrorMessage);
                }
                else if (entity is BaseDTO baseDTO)
                {
                    var (IsValid, ErrorMessage) = baseDTO.Id.ValidateId<T>(isIdEmpty.Value);
                    if (!IsValid) errorMessages.Add(ErrorMessage);
                }
                else throw new ValidationNotImplementedException<T>
                    ($"No validators were registered for class \"{typeof(T).Name}\".");
            }

            ValidationResult results = await validator.ValidateAsync(entity, cancellationToken);

            errorMessages.AddRange(results.Errors.Select(m => m.ErrorMessage));
            return (!errorMessages.Any(), errorMessages);
        }
        catch (Exception e)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"An error occured while trying to validate the \"{typeof(T).Name}\"." },
                new List<string>() {e.Message}
            });
        }
    }
    public static async Task<(bool IsValid, IEnumerable<string> Errors)> ValidateBaseUserAsync<T>
        (this T baseUser, IValidator<T> baseUserValidator, bool? isIdEmpty = null,
        CancellationToken cancellationToken = default) where T : ApplicationUser, new()
    {
        return await baseUser.ValidateEntityAsync(baseUserValidator, isIdEmpty, cancellationToken);
    }
    //private static async Task<(bool IsValid, IEnumerable<string> Errors)> ValidateBaseModelAsync<T>
    //    (this T baseModel, IValidator<T> baseModelValidator, bool? isIdZero = null,
    //    CancellationToken cancellationToken = default) where T : BaseEntity, new()
    //{
    //    return await baseModel.ValidateEntityAsync(baseModelValidator, isIdZero, cancellationToken);
    //}
    public static async Task<(bool IsValid, IEnumerable<string> Errors)> ValidateBaseDTOAsync<T>
        (this T baseDTO, IValidator<T> baseDTOValidator, bool? isIdZero = null,
        CancellationToken cancellationToken = default) where T : class, IValidatable
    {
        return await baseDTO.ValidateEntityAsync(baseDTOValidator, isIdZero, cancellationToken);
    }

    private static async Task<(bool IsValid, IEnumerable<IEnumerable<string>> ErrorsList)> ValidateEtitiesAsync<T>
        (this IEnumerable<T> entities, IValidator<T> validator, bool? isIdEmpty = null,
        CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            if (entities == null || entities.Any())
                return (false, new List<List<string>>() { new List<string>() { $"No {typeof(T).Name.Pluralize()} were sent." } });
            if (validator == null)
                throw new ValidationNotImplementedException<T>($"No validators were registered for class \"{typeof(T).Name}\".");

            bool isValid = true;
            IEnumerable<IEnumerable<string>> errorMessagesList = new List<List<string>>();

            foreach (var entity in entities)
            {
                var errorMessages = new List<string>();
                var entityValidationResults = await entity.ValidateEntityAsync(validator, isIdEmpty, cancellationToken);
                if (!entityValidationResults.IsValid) errorMessages.AddRange(entityValidationResults.Errors);
                if (isValid && errorMessages.Any()) isValid = false;
                errorMessagesList = errorMessagesList.Append(errorMessages);
            }
            return (isValid, errorMessagesList);
        }
        catch (Exception e)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"An error occured while trying to validate the \"{typeof(T).Name.Pluralize()}\"." },
                new List<string>() {e.Message}
            });
        }
    }
    public static async Task<(bool IsValid, IEnumerable<IEnumerable<string>> ErrorsList)> ValidateBaseUserListAsync<T>
        (this IEnumerable<T> baseUserList, IValidator<T> baseUserValidator,
        bool? isIdEmpty = null, CancellationToken cancellationToken = default) where T : ApplicationUser, new()
    {
        return await baseUserList.ValidateEtitiesAsync(baseUserValidator, isIdEmpty, cancellationToken);
    }
    public static async Task<(bool IsValid, IEnumerable<IEnumerable<string>> ErrorsList)> ValidateBaseModelListAsync<T>
        (this IEnumerable<T> baseModelList, IValidator<T> baseModelValidator,
        bool? isIdZero = null, CancellationToken cancellationToken = default) where T : BaseEntity, new()
    {
        return await baseModelList.ValidateEtitiesAsync(baseModelValidator, isIdZero, cancellationToken);
    }
    public static async Task<(bool IsValid, IEnumerable<IEnumerable<string>> ErrorsList)> ValidateBaseDTOlListAsync<T>
        (this IEnumerable<T> baseDTOList, IValidator<T> baseDTOValidator,
        bool? isIdZero = null, CancellationToken cancellationToken = default) where T : class, IValidatable
    {
        return await baseDTOList.ValidateEtitiesAsync(baseDTOValidator, isIdZero, cancellationToken);
    }

    public static bool ValidatePredicate<TIn, TOut>(this Expression<Func<TIn, TOut>> predicate)
    {
        try
        {
            if (predicate == null || predicate.Body == null) return false;
            predicate.Compile();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static (bool isValid, IEnumerable<string> ErrorMessages) ValidatePassword(this string password)
    {
        var ErrorMessages = new List<string>();
        if (!password.ValidateString()) ErrorMessages.Add("The password cannot be empty.");
        else
        {
            //string passwordRegex = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,}$";
            //bool isValid = Regex.IsMatch(password, passwordRegex);

            if (!Regex.IsMatch(password, @".{8,}")) ErrorMessages.Add("The password cannot be less than 8 characters long.");
            if (!Regex.IsMatch(password, @".*[A-Z].*")) ErrorMessages.Add("The password must have at least one Uppercase char.");
            if (!Regex.IsMatch(password, @".*[a-z].*")) ErrorMessages.Add("The password must have at least one Lowercase char.");
            if (!Regex.IsMatch(password, @".*\d.*")) ErrorMessages.Add("The password must have at least one Digit.");
            if (!Regex.IsMatch(password, @".*\W.*")) ErrorMessages.Add("The password must have Nonalphanumeric.");
        }
        return (!ErrorMessages.Any(), ErrorMessages.Any() ? ErrorMessages.Prepend("The password provided was not valid.") : ErrorMessages);
    }
}
