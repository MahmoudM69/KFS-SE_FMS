using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Security.Claims;
using Common.Attributes;
using DataAccess.Models.UserModels;

namespace DataAccess.Services.IRepositories;

[Service(nameof(IBaseUserRepository<T>))]
public interface IBaseUserRepository<T> where T : ApplicationUser, new()
{
    public Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<T> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<T> GetByEmailAsync(string email, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<T> CreateAsync(T user, string password, CancellationToken cancellationToken = default);
    public Task<T> CreateAsync(T user, string password, IEnumerable<Claim> claims, bool addDefaultClaims = true, CancellationToken cancellationToken = default);
    public Task<T> UpdateAsync(T user, CancellationToken cancellationToken = default);
    public Task<T> DeleteAsync(string id, CancellationToken cancellationToken = default);
    public Task<T> AddClaimsToUserAsync(string id, IEnumerable<Claim> claims, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Claim>> GetUserClaimsAsync(string id, CancellationToken cancellationToken = default);
}
