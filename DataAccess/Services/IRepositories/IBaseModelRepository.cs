using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using Common.Interfaces.Entities;

namespace DataAccess.Services.IRepositories;

[Service(nameof(IBaseModelRepository<T>))]
public interface IBaseModelRepository<T> where T : class, IBaseEntity, ISoftDeletableEntity
{
    public Task<Result<T>> GetByIdAsync(int Id, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<Result<T>> FindFirstAsync(Expression<Func<T, bool>> predicate, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<Result<IEnumerable<T>>> FindAsync(Expression<Func<T, bool>> predicate, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<Result<IEnumerable<T>>> GetAllAsync(bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<Result<T>> CreateAsync(T entity, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<T>>> CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    public Task<Result<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<T>>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    public Task<Result<T>> DeleteAsync(int Id, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<T>>> DeleteRangeAsync(IEnumerable<int> Ids, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<T>>> DeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    public Task<Result<T>> SoftDeleteAsync(int Id, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<T>>> SoftDeleteRangeAsync(IEnumerable<int> Ids, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<T>>> SoftDeleteRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    public Task<Result<T>> RecoverAsync(int Id, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<T>>> RecoverRangeAsync(IEnumerable<int> Ids, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<T>>> RecoverRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
