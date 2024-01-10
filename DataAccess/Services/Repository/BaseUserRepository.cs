using Humanizer;
using Microsoft.AspNetCore.Identity;
using Common.Exceptions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Common.Attributes;
using DataAccess.Models.UserModels;
using DataAccess.Services.IRepositories;

namespace DataAccess.Services.Repository;

[Service(nameof(IBaseUserRepository<T>))]
public class BaseUserRepository<T> : IBaseUserRepository<T> where T : ApplicationUser, new()
{
    private readonly UserManager<T> _userManager;

    public BaseUserRepository(UserManager<T> userManager)
    {
        _userManager = userManager;
    }

    public virtual async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id)) throw new BadRequestException(new List<List<string>>() {
                new List<string>() { "The Id cannot be empty/null." }
            });

            var users = _userManager.Users.AsNoTracking().Where(u => u.Id == id);

            if (users == null || !users.Any()) throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"There are no {nameof(T).Pluralize()} in the database." }
            });

            foreach (var includeProperty in includeProperties) users = users.Include(includeProperty);

            return await users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ??
                throw new NotFoundException(new List<List<string>>() { new List<string>() { $"The {nameof(T).Pluralize()} requested cannot be found." } });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }

    public async Task<T> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            var users = _userManager.Users.AsNoTracking().Where(u => u.UserName == userName);

            if (users == null || !users.Any()) throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"There are no {nameof(T).Pluralize()} in the database." }
            });

            foreach (var includeProperty in includeProperties) users = users.Include(includeProperty);

            return await users.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken) ??
                throw new NotFoundException(new List<List<string>>() { new List<string>() { $"The {nameof(T).Pluralize()} requested cannot be found." } });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }

    public async Task<T> GetByEmailAsync(string email, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            var users = _userManager.Users.AsNoTracking().Where(u => u.Email == email);

            if (users == null || !users.Any()) throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"There are no {nameof(T).Pluralize()} in the database." }
            });

            foreach (var includeProperty in includeProperties) users = users.Include(includeProperty);

            return await users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken) ??
                throw new NotFoundException(new List<List<string>>() { new List<string>() { $"The {nameof(T).Pluralize()} requested cannot be found." } });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }

    public virtual async Task<T> FindFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            var users = _userManager.Users.AsNoTracking().Where(predicate);

            if (users == null || !users.Any()) throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"There are no {nameof(T).Pluralize()} in the database." }
            });

            foreach (var includeProperty in includeProperties) users = users.Include(includeProperty);

            return await users.FirstOrDefaultAsync(predicate) ?? throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"The {nameof(T)} requested cannot be found." }
            });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            var users = _userManager.Users.AsNoTracking().Where(predicate);

            if (users == null || !users.Any()) throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"The {nameof(T).Pluralize()} requested cannot be found." }
            });

            foreach (var includeProperty in includeProperties) users.Include(includeProperty);

            return await users.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        try
        {
            var users = _userManager.Users.AsNoTracking();

            if (users == null || !users.Any()) throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"The {nameof(T).Pluralize()} requested cannot be found." }
            });
            includeProperties.Iter(ip => users.Include(ip));
            //foreach (var includeProperty in includeProperties ) users.Include(includeProperty);

            return await users.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }

    public virtual async Task<T> CreateAsync(T user, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var isValid = true;
            IEnumerable<string> errorMessages = new List<string>();

            var emailExits = await _userManager.FindByEmailAsync(user.Email!);
            if (emailExits != null) errorMessages = errorMessages.Append("The Email provided is already being used by another user.");
            var usernameExits = await _userManager.FindByNameAsync(user.UserName!);
            if (usernameExits != null) errorMessages = errorMessages.Append("The Username provided is already being used by another user.");

            if (!isValid || errorMessages.Any()) throw new BadRequestException(errorMessages);
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded) return user;

            throw new AppException(new List<List<string>>() {
                new List<string>() { $"An error while tring to create the {typeof(T).Name}." },
                    result.Errors.Select(x => x.Description).ToList()
            });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }

    public virtual async Task<T> CreateAsync(T user, string password, IEnumerable<Claim> claims, bool addDefaultClaims = true, CancellationToken cancellationToken = default)
    {
        try
        {
            var newUser = await CreateAsync(user, password, cancellationToken);

            claims ??= new List<Claim>();
            var claimsWithDefaults = addDefaultClaims ? new List<Claim>(claims)
            {
                new(nameof(newUser.Id), newUser.Id),
                new(nameof(newUser.Email), newUser.Email!),
                new(nameof(newUser.UserName), newUser.UserName!),
                //new(nameof(user.FirstName), user.FirstName),
                //new(nameof(user.LastName), user.LastName),
                new("Auth", typeof(T).Name),
            } : null;

            var claimsResult = addDefaultClaims ? await _userManager.AddClaimsAsync(newUser, claimsWithDefaults!) :
                await _userManager.AddClaimsAsync(newUser, claims);
            return claimsResult.Succeeded ? newUser : throw new AppException(new List<List<string>>() {
                new List<string>() { $"An error while tring to add the claims to the {typeof(T).Name}." },
                    claimsResult.Errors.Select(x => x.Description).ToList()
            });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() { new List<string>() { $"{ex.Message}." } });
        }
    }

    public virtual async Task<T> UpdateAsync(T user, CancellationToken cancellationToken = default)
    {
        try
        {
            var isValid = true;
            IEnumerable<string> errorMessages = new List<string>();

            var emailExits = await _userManager.FindByEmailAsync(user.Email!);
            if (emailExits != null && emailExits.Id != user.Id)
                errorMessages = errorMessages.Append("The Email provided is already being used by another user.");
            var usernameExits = await _userManager.FindByNameAsync(user.UserName!);
            if (usernameExits != null && usernameExits.Id != user.Id)
                errorMessages = errorMessages.Append("The Username provided is already being used by another user.");

            if (!isValid || errorMessages.Any()) throw new BadRequestException(errorMessages);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return user;
            throw new AppException(new List<List<string>>() {
                new List<string>() { $"An error while tring to create the {nameof(T)}" },
                result.Errors.Select(x => x.Description).ToList()
            });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }

    public virtual async Task<T> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id)) throw new BadRequestException("The Id cannot be empty.");
            var user = await _userManager.FindByIdAsync(id) ??
                throw new NotFoundException(new List<List<string>>() {
                    new List<string>() { $"The {typeof(T).Name} you're trying to update was not found." }
                });

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded) return user;

            throw new AppException(new List<List<string>>() {
                new List<string>() { $"An error while tring to create the {nameof(T)}" },
                result.Errors.Select(x => x.Description).ToList()
            });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() { new List<string>() { $"{ex.Message}." } });
        }
    }
    public virtual async Task<T> AddClaimsToUserAsync(string id, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new BadRequestException(new List<List<string>>() { new List<string>() { "The Id cannot be empty/null." } });

            var user = await _userManager.FindByIdAsync(id) ?? throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"The {typeof(T).Name} requested cannot be found." }
            });

            var claimsResult = await _userManager.AddClaimsAsync(user, claims);
            return claimsResult.Succeeded ? user : throw new AppException(new List<List<string>>() {
                new List<string>() { $"An error while tring to add the claims to the {typeof(T).Name}." },
                    claimsResult.Errors.Select(x => x.Description).ToList()
            });
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() { new List<string>() { $"{ex.Message}." } });
        }
    }

    public virtual async Task<IEnumerable<Claim>> GetUserClaimsAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id)) throw new BadRequestException(
                new List<List<string>>() { new List<string>() { "The Id cannot be empty/null." } }
                );

            var user = await _userManager.FindByIdAsync(id) ?? throw new NotFoundException(new List<List<string>>() {
                new List<string>() { $"The {typeof(T).Name} requested cannot be found." }
            });

            var claims = await _userManager.GetClaimsAsync(user);
            return claims != null && claims.Any() ? claims.ToList() : throw new NotFoundException(
                new List<string>() { $"Couldn't find any claims for the {typeof(T).Name}." }
            );
        }
        catch (Exception ex)
        {
            throw new ServerException(new List<List<string>>() {
                new List<string>() { $"{ex.Message}." }
            });
        }
    }
}
