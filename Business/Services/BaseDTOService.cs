using AutoMapper;
using Business.Helpers.Extensions;
using Business.IServices;
using FluentValidation;
using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using Common.Exceptions;
using Humanizer;
using DataAccess.Services.IRepositories;
using Common.Interfaces.Entities;
using Common.Interfaces.DTOs;
using Common.Interfaces;
using AutoMapper.Extensions.ExpressionMapping;

namespace Business.Services;

[Service(nameof(IBaseDTOService<Tdto, Tmodel, Tirepository>))]
public class BaseDTOService<Tdto, Tmodel, Tirepository> : IBaseDTOService<Tdto, Tmodel, Tirepository>
    where Tdto : class, IBaseDTO, ISoftDeletableDTO, IValidatable
    where Tmodel : class, IBaseEntity, ISoftDeletableEntity
    where Tirepository : IBaseModelRepository<Tmodel>
{
    private readonly Tirepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<Tdto>? _validator;

    public BaseDTOService(Tirepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public BaseDTOService(Tirepository repository, IMapper mapper, IValidator<Tdto> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<Tdto>> GetByIdAsync(int id, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Tdto, object>>[] includeProperties)
    {
        try
        {
            var (IsValid, ErrorMessage) = id.ValidateId<INullable>();
            if (!IsValid) return new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
            return (await _repository.GetByIdAsync(id, filterSoftDelete,
                cancellationToken, includeProperties.Select(_mapper.MapExpression<Expression<Func<Tmodel, object>>>).ToArray())).Match(
                    succ => _mapper.Map<Tdto>(succ),
                    excp => new Result<Tdto>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to get the requested {typeof(Tdto).Name}.", ex.Message
            }));
        }
    }

    public async Task<Result<Tdto>> FindFirstAsync(Expression<Func<Tdto, bool>> predicate, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Tdto, object>>[] includeProperties)
    {
        try
        {
            var isValid = predicate.ValidatePredicate();
            if (!isValid) return new(new BadRequestException(new List<string>() { "The expression predicate sent was not valid." }));
            return (await _repository.FindFirstAsync(_mapper.MapExpression<Expression<Func<Tmodel, bool>>>(predicate), filterSoftDelete,
                cancellationToken, includeProperties.Select(_mapper.MapExpression<Expression<Func<Tmodel, object>>>).ToArray())).Match(
                    succ => _mapper.Map<Tdto>(succ),
                    excp => new Result<Tdto>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to get the requested {typeof(Tdto).Name}.", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> FindAsync(Expression<Func<Tdto, bool>> predicate, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Tdto, object>>[] includeProperties)
    {
        try
        {
            var isValid = predicate.ValidatePredicate();
            if (!isValid) return new(new BadRequestException(new List<string>() { "The expression predicate sent was not valid." }));
            return (await _repository.FindAsync(_mapper.MapExpression<Expression<Func<Tmodel, bool>>>(predicate), filterSoftDelete,
                cancellationToken, includeProperties.Select(_mapper.MapExpression<Expression<Func<Tmodel, object>>>).ToArray())).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to get the requested {typeof(Tdto).Name.Pluralize()}.", ex.Message
            }));
        }

    }

    public async Task<Result<IEnumerable<Tdto>>> GetAllAsync(bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Tdto, object>>[] includeProperties)
    {
        try
        {
            return (await _repository.GetAllAsync(filterSoftDelete,
                cancellationToken, includeProperties.Select(_mapper.MapExpression<Expression<Func<Tmodel, object>>>).ToArray())).Match(
                succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                excp => new Result<IEnumerable<Tdto>>(excp));
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to get the requested {typeof(Tdto).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public async Task<Result<Tdto>> CreateAsync(Tdto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsIdValid, IdErrorMessage) = dto.Id.ValidateId<INullable>(true);
            if (_validator != null)
            {
                var (IsValid, Errors) = await dto.ValidateBaseDTOAsync(_validator, true, cancellationToken);
                Errors = string.IsNullOrEmpty(IdErrorMessage) ? Errors : Errors.Prepend(IdErrorMessage);
                if (!IsValid || Errors.Any()) return new(new BadRequestException(Errors));
            }
            if (!IsIdValid) return new(new BadRequestException(new List<string>() { IdErrorMessage }));

            return (await _repository.CreateAsync(_mapper.Map<Tmodel>(dto), cancellationToken)).Match(
                    succ => _mapper.Map<Tdto>(succ),
                    excp => new Result<Tdto>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to create the \"{typeof(Tdto).Name}\".", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> CreateRangeAsync(IEnumerable<Tdto> dtos, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsIdsValid, IdErrorsMessage) = dtos.Select(x => x.Id).ValidateIds<INullable>(true);
            if (_validator != null)
            {
                var (IsValid, ErrorsList) = await dtos.ValidateBaseDTOlListAsync(_validator, true, cancellationToken);
                ErrorsList = (IsIdsValid) ? ErrorsList.Prepend(IdErrorsMessage) : ErrorsList;
                if (!IsValid) return new(new BadRequestException(ErrorsList));
            }
            if (IsIdsValid) return new(new BadRequestException(IdErrorsMessage));

            return (await _repository.CreateRangeAsync(_mapper.Map<IEnumerable<Tmodel>>(dtos), cancellationToken)).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to create the \"{typeof(Tdto).Name.Pluralize()}\".", ex.Message
            }));
        }
    }

    public async Task<Result<Tdto>> UpdateAsync(Tdto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsIdValid, IdErrorMessage) = dto.Id.ValidateId<INullable>(true);
            if (_validator != null)
            {
                var (IsValid, Errors) = await dto.ValidateBaseDTOAsync(_validator, true, cancellationToken);
                Errors = string.IsNullOrEmpty(IdErrorMessage) ? Errors : Errors.Prepend(IdErrorMessage);
                if (!IsValid || Errors.Any()) return new(new BadRequestException(Errors));
            }
            if (IsIdValid) return new(new BadRequestException(new List<string>() { IdErrorMessage }));

            return (await _repository.UpdateAsync(_mapper.Map<Tmodel>(dto), cancellationToken)).Match(
                    succ => _mapper.Map<Tdto>(succ),
                    excp => new Result<Tdto>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to update the \"{typeof(Tdto).Name}\".", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> UpdateRangeAsync(IEnumerable<Tdto> dtos, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsIdsValid, IdErrorsMessage) = dtos.Select(x => x.Id).ValidateIds<INullable>(true);
            if (_validator != null)
            {
                var (IsValid, ErrorsList) = await dtos.ValidateBaseDTOlListAsync(_validator, true, cancellationToken);
                ErrorsList = (IsIdsValid) ? ErrorsList.Prepend(IdErrorsMessage) : ErrorsList;
                if (!IsValid) return new(new BadRequestException(ErrorsList));
            }
            if (IsIdsValid) return new(new BadRequestException(IdErrorsMessage));

            return (await _repository.UpdateRangeAsync(_mapper.Map<IEnumerable<Tmodel>>(dtos), cancellationToken)).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to update the \"{typeof(Tdto).Name.Pluralize()}\".", ex.Message
            }));
        }
    }

    public async Task<Result<Tdto>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsValid, ErrorMessage) = id.ValidateId<INullable>();
            if (!IsValid) return new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
            return (await _repository.DeleteAsync(id, cancellationToken)).Match(
                    succ => _mapper.Map<Tdto>(succ),
                    excp => new Result<Tdto>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to delete the requested {typeof(Tdto).Name}.", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsValid, ErrorMessages) = ids.ValidateIds<INullable>();
            if (!IsValid) return new(new BadRequestException(new List<IEnumerable<string>>() {
                new List<string>() { "Some of the ids sent were not valid." },
                ErrorMessages
            }));

            return (await _repository.DeleteRangeAsync(ids, cancellationToken)).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to delete the requested {typeof(Tdto).Name}.", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> DeleteRangeAsync(Expression<Func<Tdto, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            var isValid = predicate.ValidatePredicate();
            if (!isValid) return new(new BadRequestException(new List<string>() { "The expression predicate sent was not valid." }));

            return (await _repository.DeleteRangeAsync(_mapper.MapExpression<Expression<Func<Tmodel, bool>>>(predicate), cancellationToken)).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to delete the requested {typeof(Tdto).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public async Task<Result<Tdto>> RecoverAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsValid, ErrorMessage) = id.ValidateId<INullable>();
            if (!IsValid) return new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
            return (await _repository.RecoverAsync(id, cancellationToken)).Match(
                    succ => _mapper.Map<Tdto>(succ),
                    excp => new Result<Tdto>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to recover the requested {typeof(Tdto).Name}.", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> RecoverRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsValid, ErrorMessages) = ids.ValidateIds<INullable>();
            if (!IsValid) return new(new BadRequestException(new List<IEnumerable<string>>() {
                new List<string>() { "Some of the ids sent were not valid." },
                ErrorMessages
            }));

            return (await _repository.RecoverRangeAsync(ids, cancellationToken)).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to recover the requested {typeof(Tdto).Name}.", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> RecoverRangeAsync(Expression<Func<Tdto, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            var isValid = predicate.ValidatePredicate();
            if (!isValid) return new(new BadRequestException(new List<string>() { "The expression predicate sent was not valid." }));

            return (await _repository.RecoverRangeAsync(_mapper.MapExpression<Expression<Func<Tmodel, bool>>>(predicate), cancellationToken)).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to recover the requested {typeof(Tdto).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public async Task<Result<Tdto>> SoftDeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsValid, ErrorMessage) = id.ValidateId<INullable>();
            if (!IsValid) return new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
            return (await _repository.SoftDeleteAsync(id, cancellationToken)).Match(
                    succ => _mapper.Map<Tdto>(succ),
                    excp => new Result<Tdto>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to soft delete the requested {typeof(Tdto).Name}.", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> SoftDeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        try
        {
            var (IsValid, ErrorMessages) = ids.ValidateIds<INullable>();
            if (!IsValid) return new(new BadRequestException(new List<IEnumerable<string>>() {
                new List<string>() { "Some of the ids sent were not valid." },
                ErrorMessages
            }));

            return (await _repository.SoftDeleteRangeAsync(ids, cancellationToken)).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to soft delete the requested {typeof(Tdto).Name}.", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Tdto>>> SoftDeleteRangeAsync(Expression<Func<Tdto, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            var isValid = predicate.ValidatePredicate();
            if (!isValid) return new(new BadRequestException(new List<string>() { "The expression predicate sent was not valid." }));

            return (await _repository.SoftDeleteRangeAsync(_mapper.MapExpression<Expression<Func<Tmodel, bool>>>(predicate), cancellationToken)).Match(
                    succ => new Result<IEnumerable<Tdto>>(_mapper.Map<IEnumerable<Tdto>>(succ)),
                    excp => new Result<IEnumerable<Tdto>>(excp)
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to Soft Delete the requested {typeof(Tdto).Name.Pluralize()}.", ex.Message
            }));
        }
    }
}
