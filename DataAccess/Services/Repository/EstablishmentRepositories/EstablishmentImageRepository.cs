using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using System.Linq.Expressions;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;
using DataAccess.Services.Repository.SharedRepositories;
using DataAccess.Models.EstablishmentModels;

namespace DataAccess.Services.Repository.EstablishmentRepositories;

[Service(nameof(IEstablishmentImageRepository))]
public class EstablishmentImageRepository : BaseImageRepository<EstablishmentImage>, IEstablishmentImageRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public EstablishmentImageRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Result<IEnumerable<EstablishmentImage>>> GetEstablishmentImages(int establishmentId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<EstablishmentImage, object>>[] includeProperties)
    {
        try
        {
            return await base.FindAsync(x => x.EstablishmentId == establishmentId, filterSoftDelete, cancellationToken, includeProperties);
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occured while trying to get the \"Images\".", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<EstablishmentImage>>> DeleteEstablishmentImages(int establishmentId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await (await base.FindAsync(x => x.EstablishmentId == establishmentId, filterSoftDelete, cancellationToken)).Match(
                    async succ => await base.DeleteRangeAsync(succ.Select(x => x.Id), cancellationToken),
                    excp => new Task<Result<IEnumerable<EstablishmentImage>>>(() => new Result<IEnumerable<EstablishmentImage>>(excp))
                );
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occured while trying to delete the \"Images\".", ex.Message
            }));
        }
    }
}
