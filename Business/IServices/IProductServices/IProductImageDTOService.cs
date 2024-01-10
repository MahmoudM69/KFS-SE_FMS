using DTO.DTOs.ProductDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Common.Attributes;
using LanguageExt.Common;
using System.Linq.Expressions;
using System;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Models.ProductModels;

namespace Business.IServices.IProductServices;

[Service(nameof(IProductImageDTOService))]
public interface IProductImageDTOService : IBaseDTOService<ProductImageDTO, ProductImage, IProductImageRepository>
{
    public Task<Result<IEnumerable<ProductImageDTO>>> GetProductImages(int productId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<ProductImageDTO, object>>[] includeProperties);
    public Task<Result<IEnumerable<ProductImageDTO>>> DeleteProductImagesByProductId(int productId, CancellationToken cancellationToken = default);
}
