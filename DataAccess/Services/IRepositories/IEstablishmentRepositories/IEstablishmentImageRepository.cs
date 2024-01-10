using Common.Attributes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using System.Linq.Expressions;
using System;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Services.IRepositories.ISharedRepositories;

namespace DataAccess.Services.IRepositories.IEstablishmentRepositories;

[Service(nameof(IEstablishmentImageRepository))]
public interface IEstablishmentImageRepository : IBaseImageRepository<EstablishmentImage>
{
    public Task<Result<IEnumerable<EstablishmentImage>>> GetEstablishmentImages(int establishmentId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<EstablishmentImage, object>>[] includeProperties);
    public Task<Result<IEnumerable<EstablishmentImage>>> DeleteEstablishmentImages(int establishmentId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default);
}
