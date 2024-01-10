using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Common.Classes;
using Common.Attributes;
using LanguageExt.Common;
using DataAccess.Services.IRepositories;
using Common.Interfaces.Entities;
using Common.Interfaces.DTOs;

namespace Business.IServices;

[Service(nameof(IBaseDTOService<Tdto, Tmodel, Tirepository>))]
public interface IBaseDTOService<Tdto, Tmodel, Tirepository>
    where Tdto : class, IBaseDTO, ISoftDeletableDTO
    where Tmodel : class, IBaseEntity, ISoftDeletableEntity
    where Tirepository : IBaseModelRepository<Tmodel>
{
    public Task<Result<Tdto>> GetByIdAsync(int id, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Tdto, object>>[] includeProperties);
    public Task<Result<Tdto>> FindFirstAsync(Expression<Func<Tdto, bool>> predicate, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Tdto, object>>[] includeProperties);
    public Task<Result<IEnumerable<Tdto>>> FindAsync(Expression<Func<Tdto, bool>> predicate, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Tdto, object>>[] includeProperties);
    public Task<Result<IEnumerable<Tdto>>> GetAllAsync(bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Tdto, object>>[] includeProperties);
    public Task<Result<Tdto>> CreateAsync(Tdto entity, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<Tdto>>> CreateRangeAsync(IEnumerable<Tdto> entities, CancellationToken cancellationToken = default);
    public Task<Result<Tdto>> UpdateAsync(Tdto entity, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<Tdto>>> UpdateRangeAsync(IEnumerable<Tdto> entities, CancellationToken cancellationToken = default);
    public Task<Result<Tdto>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<Tdto>>> DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<Tdto>>> DeleteRangeAsync(Expression<Func<Tdto, bool>> predicate, CancellationToken cancellationToken = default);
    public Task<Result<Tdto>> RecoverAsync(int id, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<Tdto>>> RecoverRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<Tdto>>> RecoverRangeAsync(Expression<Func<Tdto, bool>> predicate, CancellationToken cancellationToken = default);
    public Task<Result<Tdto>> SoftDeleteAsync(int id, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<Tdto>>> SoftDeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<Tdto>>> SoftDeleteRangeAsync(Expression<Func<Tdto, bool>> predicate, CancellationToken cancellationToken = default);
}
