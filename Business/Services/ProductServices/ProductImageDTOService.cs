using AutoMapper;
using Business.Helpers.Extensions;
using Business.IServices.IProductServices;
using DTO.DTOs.ProductDTOs;
using FluentValidation;
using Common.Attributes;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using System.Linq.Expressions;
using System;
using Common.Exceptions;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Models.ProductModels;

namespace Business.Services.ProductServices;

[Service(nameof(IProductImageDTOService))]
public class ProductImageDTOService : BaseDTOService<ProductImageDTO, ProductImage, IProductImageRepository>, IProductImageDTOService
{
    private readonly IProductImageRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductImageDTO>? _validator;

    public ProductImageDTOService(IProductImageRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public ProductImageDTOService(IProductImageRepository repository, IMapper mapper, IValidator<ProductImageDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<ProductImageDTO>>> GetProductImages(int productId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<ProductImageDTO, object>>[] includeProperties)
    {
        var (IsValid, ErrorMessage) = productId.ValidateId<INullable>();
        return IsValid ? await base.FindAsync(x => x.ProductId == productId, filterSoftDelete,
            cancellationToken, includeProperties) :
            new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
    }

    public async Task<Result<IEnumerable<ProductImageDTO>>> DeleteProductImagesByProductId(
        int productId, CancellationToken cancellationToken = default)
    {
        var (IsValid, ErrorMessage) = productId.ValidateId<INullable>();
        return IsValid ? await base.DeleteRangeAsync(x => x.ProductId == productId, cancellationToken) :
            new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
    }
}
