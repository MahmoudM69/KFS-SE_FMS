using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using DataAccess.Models.SharedModels;
using DataAccess.Services.IRepositories.ISharedRepositories;
using Common.Interfaces.Entities;

namespace DataAccess.Services.Repository.SharedRepositories;

[Service(nameof(IBaseImageRepository<T>))]
public class BaseImageRepository<T> : BaseModelRepository<T>, IBaseImageRepository<T> where T : class, IBaseEntity, ISoftDeletableEntity
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public BaseImageRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Task<Result<T>> DeleteImageByImageUrl(string imageUrl, bool filterSoftDelete = true, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    //public async Task<Result<T>> DeleteImageByImageUrl(string imageUrl, bool filterSoftDelete = true, CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        throw new Not();
    //        //return await (await base.FindFirstAsync(x => x.ImageUrl == imageUrl, filterSoftDelete, cancellationToken)).Match(
    //        //    async succ => await base.DeleteAsync(succ.Id, cancellationToken),
    //        //    excp => new Task<Result<T>>(() => new Result<T>(excp))
    //        //);
    //    }
    //    catch (Exception ex)
    //    {
    //        return new(new ServerException(new List<string>() {
    //            $"An error occured while trying to delete the \"{typeof(T).Name}\".", ex.Message
    //        }));
    //    }
    //}
}
