using DataAccess.Services.IRepositories;
using Humanizer;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using Common.Exceptions;
using Common.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Services.Repository;

[Service(nameof(IBaseModelRepository<T>))]
public class BaseModelRepository<T> : IBaseModelRepository<T> where T : class, IBaseEntity, ISoftDeletableEntity
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;
    public BaseModelRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public virtual async Task<Result<T>> GetByIdAsync(int id, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            if (id < 1) return new(new BadRequestException("The id sent was not valid."));

            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = filterSoftDelete;

                IQueryable<T> query = context.Set<T>().AsNoTracking().Where(x => x.Id == id);

                if (query is null || !query.Any()) return new(new NotFoundException(new List<List<string>>() {
                    new List<string>() { $"There are no {nameof(T).Pluralize()} in the database." }
                }));

                foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

                return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ??
                    new Result<T>(new NotFoundException(new List<string>() { $"Couldn't find the requested {typeof(T).Name}." }));
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to get the requested {typeof(T).Name}.", ex.Message
            }));
        }
    }
    public virtual async Task<Result<T>> FindFirstAsync(Expression<Func<T, bool>> predicate, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = filterSoftDelete;

                IQueryable<T> query = context.Set<T>().AsNoTracking().Where(predicate);

                if (query is null || !query.Any()) return new(new NotFoundException(new List<List<string>>() {
                    new List<string>() { $"There are no {nameof(T).Pluralize()} in the database." }
                }));

                foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

                return await query.FirstOrDefaultAsync(predicate, cancellationToken) ??
                    new Result<T>(new NotFoundException(new List<string>() {
                        $"Couldn't find the {typeof(T).Name} based on this Expression Predicate."
                    }));
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to get the requested  {typeof(T).Name}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> FindAsync(Expression<Func<T, bool>> predicate, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = filterSoftDelete;

                IQueryable<T> query = context.Set<T>().AsNoTracking().Where(predicate);

                if (query is null || !query.Any()) return new(new NotFoundException(new List<List<string>>() {
                    new List<string>() { $"There are no {nameof(T).Pluralize()} in the database." }
                }));

                foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

                return await query.ToListAsync(cancellationToken) ??
                    new Result<IEnumerable<T>>(new NotFoundException(new List<string>() {
                        $"Couldn't find any \"{typeof(T).Name.Pluralize()}\" based on this Expression Predicate."
                    }));
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to get the requested {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> GetAllAsync(bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = filterSoftDelete;

                IQueryable<T> query = context.Set<T>().AsNoTracking();

                if (query is null || !query.Any()) return new(new NotFoundException(new List<List<string>>() {
                    new List<string>() { $"There are no {typeof(T).Name.Pluralize()} in the database." }
                }));

                foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

                return await query.ToListAsync(cancellationToken) ??
                    new Result<IEnumerable<T>>(new NotFoundException(new List<string>() {
                        $"Couldn't find any \"{typeof(T).Name.Pluralize()}\" in the database."
                    }));
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to get the requested {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<T>> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                var dbEntity = context.Set<T>().Attach(entity);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? dbEntity.Entity : new Result<T>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to create the \"{typeof(T).Name}\".", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.Set<T>().AttachRange(entities);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? entities.ToList() : new Result<IEnumerable<T>>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to create the \"{typeof(T).Name.Pluralize()}\".", ex.Message
            }));
        }
    }

    public virtual async Task<Result<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                var dbEntity = await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
                if (dbEntity is null) return new(new NotFoundException(new List<string>() {
                    $"The {typeof(T).Name} you're trying to update was not found."
                }));
                var efEntity = context.Set<T>().Update(entity);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? efEntity.Entity : new Result<T>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to update the {typeof(T).Name}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            if (entities is null || !entities.Any())
                return new(new BadRequestException(new List<string>() { $"No {typeof(T).Name.Pluralize()} were sent." }));

            var ids = entities.Select(x => x.Id).Where(x => x > 0);
            if (ids is null || !ids.Any())
                return new(new BadRequestException(new List<string>() { $"{typeof(T).Name.Pluralize()}'s Id should not be zero." }));

            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                var dbEntities = await context.Set<T>().AsNoTracking()
                    .Where(x => x.SoftDelete >= 0 && ids.Contains(x.Id)).ToListAsync(cancellationToken);

                if (dbEntities is null || !dbEntities.Any())
                    return new(new NotFoundException(new List<string>() {
                        $"The {typeof(T).Name.Pluralize()} requested were not found."
                    }));

                bool isRequestValid = true;
                var errorMessagesList = new List<List<string>>();
                foreach (var entity in entities)
                {
                    var errorMessages = new List<string>();
                    if (!dbEntities.Select(x => x.Id).Contains(entity.Id))
                        errorMessages.Add($"Couldn't find the {typeof(T).Name} with the Id ({entity.Id}) in the database.");
                    errorMessagesList.Add(errorMessages);
                }
                if (!isRequestValid) return new(new BadRequestException(errorMessagesList));
                context.Set<T>().UpdateRange(entities);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? entities.ToList() : new Result<IEnumerable<T>>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to update the {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<T>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (id < 1) return new(new BadRequestException("The id sent was not valid."));

            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;
                var dbEntity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (dbEntity is null) return new(new NotFoundException(new List<string>() {
                            $"Couldn't find the requested {typeof(T).Name} to be deleted."
                        }));
                dbEntity.IsRecoverable = false;
                var deletedEntity = context.Set<T>().Remove(dbEntity);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? deletedEntity.Entity : new Result<T>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to delete the requested {typeof(T).Name}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        try
        {
            if (ids.Any(x => x < 1)) return new(new BadRequestException("Some of the ids sent were not valid."));

            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;
                var dbEntities = await context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
                if (dbEntities is null || !dbEntities.Any()) return new(new NotFoundException(new List<string>() {
                    $"Couldn't find any {typeof(T).Name} from the requested {typeof(T).Name.Pluralize()} in the database to be deleted."
                }));

                var notFoundIds = ids.Where(id => dbEntities.All(en => en.Id != id));
                if (notFoundIds != null && notFoundIds.Any()) return new(new NotFoundException(new List<string>(
                    notFoundIds.Select(x => $"Couldn't find the {typeof(T).Name} with the id \"{x}\"."
                ))));

                dbEntities.ForEach(e => e.IsRecoverable = false);
                context.Set<T>().RemoveRange(dbEntities);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? dbEntities.ToList() : new Result<IEnumerable<T>>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to delete the requested {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> DeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;
                var dbEntities = await context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
                if (dbEntities is null || !dbEntities.Any()) return new(new NotFoundException(new List<string>() {
                            $"Couldn't find any {typeof(T).Name} from the requested {typeof(T).Name.Pluralize()} " +
                            $"based on the Expression Predicate in the database to be deleted."
                        }));

                dbEntities.ForEach(e => e.IsRecoverable = false);
                context.Set<T>().RemoveRange(dbEntities);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? dbEntities.ToList() : new Result<IEnumerable<T>>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to delete the requested {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<T>> SoftDeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (id < 1) return new(new BadRequestException("The id sent was not valid."));

            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;
                var dbEntity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (dbEntity is null) return new(new NotFoundException(new List<string>() {
                        $"Couldn't find the requested {typeof(T).Name} to be soft-deleted."
                    }));

                if (dbEntity.SoftDelete < 0) return new(new BadRequestException(new List<string>() {
                    $"The requested {typeof(T).Name} is already soft-deleted."
                }));

                var deletedEntity = context.Set<T>().Remove(dbEntity);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? deletedEntity.Entity : new Result<T>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to soft-delete the requested {typeof(T).Name}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> SoftDeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        try
        {
            if (ids.Any(x => x < 1)) return new(new BadRequestException("Some of the ids sent were not valid."));

            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;
                var dbEntities = await context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
                if (dbEntities is null || !dbEntities.Any()) return new(new NotFoundException(new List<string>() {
                    $"Couldn't find any {typeof(T).Name} from the requested {typeof(T).Name.Pluralize()} " +
                    $"in the database to be soft-deleted."
                }));

                var notFoundIds = ids.Where(id => dbEntities.All(en => en.Id != id));
                if (notFoundIds != null && notFoundIds.Any()) return new(new NotFoundException(new List<string>(
                    notFoundIds.Select(x => $"Couldn't find the {typeof(T).Name} with the id \"{x}\"."
                ))));

                dbEntities.ForEach(x => x.SoftDelete = 0);

                context.Set<T>().RemoveRange(dbEntities);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? dbEntities.ToList() : new Result<IEnumerable<T>>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to soft-delete the requested {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> SoftDeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;
                var dbEntities = await context.Set<T>().Where(predicate).ToListAsync(cancellationToken);

                if (dbEntities is null || !dbEntities.Any()) return new(new NotFoundException(new List<string>() {
                    $"Couldn't find any {typeof(T).Name} from the requested {typeof(T).Name.Pluralize()} " +
                    $"based on the Expression Predicate in the database to be soft-deleted."
                }));

                if (dbEntities.Any(x => x.SoftDelete >= 0)) return new(new BadRequestException(new List<string>(dbEntities.Select(x =>
                    $"The requested {typeof(T).Name} with the Id \"{x.Id}\" is already soft-deleted."
                ))));

                context.Set<T>().RemoveRange(dbEntities);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? dbEntities.ToList() : new Result<IEnumerable<T>>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to soft-delete the requested {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<T>> RecoverAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (id < 1) return new(new BadRequestException("The id sent was not valid."));

            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;
                var dbEntity = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (dbEntity is null) return new(new NotFoundException(new List<string>() {
                        $"Couldn't find the requested {typeof(T).Name} to be recovered."
                    }));

                if (dbEntity.SoftDelete >= 0) return new(new BadRequestException(new List<string>() {
                    $"The requested {typeof(T).Name} is not soft-deleted."
                }));

                dbEntity.SoftDelete = 1;
                var deletedEntity = context.Set<T>().Remove(dbEntity);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? deletedEntity.Entity : new Result<T>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to recover the requested {typeof(T).Name}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> RecoverRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        try
        {
            if (ids.Any(x => x < 1)) return new(new BadRequestException("Some of the ids sent were not valid."));

            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;

                var dbEntities = await context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);

                if (dbEntities is null || !dbEntities.Any()) return new(new NotFoundException(new List<string>() {
                    $"Couldn't find any {typeof(T).Name} from the requested {typeof(T).Name.Pluralize()} " +
                    $"in the database to be recovered."
                }));

                var notFoundIds = ids.Where(id => dbEntities.All(en => en.Id != id));
                if (notFoundIds != null && notFoundIds.Any()) return new(new NotFoundException(new List<string>(notFoundIds.Select(x =>
                    $"Couldn't find the {typeof(T).Name} with the id \"{x}\"."
                ))));

                if (dbEntities.Any(x => x.SoftDelete >= 0)) return new(new BadRequestException(new List<string>(
                    dbEntities.Select(x => $"The requested {typeof(T).Name} with the Id \"{x.Id}\" is not soft-deleted."
                ))));

                dbEntities.ForEach(e => e.SoftDelete = 1);
                context.Set<T>().RemoveRange(dbEntities);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? dbEntities.ToList() : new Result<IEnumerable<T>>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to recover the requested {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }

    public virtual async Task<Result<IEnumerable<T>>> RecoverRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
            {
                context.FilterSoftDelete = false;
                var dbEntities = await context.Set<T>().Where(predicate).ToListAsync(cancellationToken);

                if (dbEntities is null || !dbEntities.Any()) return new(new NotFoundException(new List<string>() {
                    $"Couldn't find any {typeof(T).Name} from the requested {typeof(T).Name.Pluralize()} " +
                    $"based on the Expression Predicate in the database to be recovered."
                }));

                if (dbEntities.Any(x => x.SoftDelete >= 0)) return new(new BadRequestException(new List<string>(dbEntities.Select(x =>
                    $"The requested {typeof(T).Name} with the Id \"{x.Id}\" is not soft-deleted."
                ))));

                dbEntities.ForEach(e => e.SoftDelete = 1);
                context.Set<T>().RemoveRange(dbEntities);
                var results = await context.SaveChangesAsync(cancellationToken);
                return results > 0 ? dbEntities.ToList() : new Result<IEnumerable<T>>(new DbUpdateException());
            }
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occurred while trying to recover the requested {typeof(T).Name.Pluralize()}.", ex.Message
            }));
        }
    }
}
