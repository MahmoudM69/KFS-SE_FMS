using DTO.DTOs.EstablishmentDTOs;
using Common.Attributes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using LanguageExt.Common;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;
using DataAccess.Models.EstablishmentModels;

namespace Business.IServices.IEstablishmentServices;

[Service(nameof(IEstablishmentImageDTOService))]
public interface IEstablishmentImageDTOService : IBaseDTOService<EstablishmentImageDTO, EstablishmentImage, IEstablishmentImageRepository>
{
    public Task<Result<IEnumerable<EstablishmentImageDTO>>> GetEstablishmentImages(int establishmentId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<EstablishmentImageDTO, object>>[] includeProperties);
    public Task<Result<IEnumerable<EstablishmentImageDTO>>> DeleteEstablishmentImages(int establishmentId,
        CancellationToken cancellationToken = default);
}
