using AutoMapper;
using Business.Helpers.Extensions;
using Business.IServices.IEstablishmentServices;
using DTO.DTOs.EstablishmentDTOs;
using FluentValidation;
using Common.Attributes;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using LanguageExt.Common;
using Common.Exceptions;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;
using DataAccess.Models.EstablishmentModels;

namespace Business.Services.EstablishmentServices;

[Service(nameof(IEstablishmentImageDTOService))]
public class EstablishmentImageDTOService : BaseDTOService<EstablishmentImageDTO, EstablishmentImage, IEstablishmentImageRepository>,
    IEstablishmentImageDTOService
{
    private readonly IEstablishmentImageRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<EstablishmentImageDTO>? _validator;

    public EstablishmentImageDTOService(IEstablishmentImageRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public EstablishmentImageDTOService(
        IEstablishmentImageRepository repository, IMapper mapper, IValidator<EstablishmentImageDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<EstablishmentImageDTO>>> GetEstablishmentImages(int establishmentId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<EstablishmentImageDTO, object>>[] includeProperties)
    {
        var (IsValid, ErrorMessage) = establishmentId.ValidateId<INullable>();
        return IsValid ? await base.FindAsync(x => x.EstablishmentId == establishmentId, filterSoftDelete,
            cancellationToken, includeProperties) :
                new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
    }

    public async Task<Result<IEnumerable<EstablishmentImageDTO>>> DeleteEstablishmentImages(int establishmentId,
        CancellationToken cancellationToken = default)
    {
        var (IsValid, ErrorMessage) = establishmentId.ValidateId<INullable>();
        return IsValid ? await base.DeleteRangeAsync(x => x.EstablishmentId == establishmentId, cancellationToken) :
                new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
    }
}
