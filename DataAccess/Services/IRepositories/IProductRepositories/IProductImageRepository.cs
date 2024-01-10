using Common.Attributes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using System.Linq.Expressions;
using System;
using DataAccess.Models.ProductModels;
using DataAccess.Services.IRepositories.ISharedRepositories;

namespace DataAccess.Services.IRepositories.IProductRepositories;

[Service(nameof(IProductImageRepository))]
public interface IProductImageRepository : IBaseImageRepository<ProductImage>
{
    public Task<Result<IEnumerable<ProductImage>>> GetProductImages(int productId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<ProductImage, object>>[] includeProperties);
    public Task<Result<IEnumerable<ProductImage>>> DeleteProductImagesByProductId(int productId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default);
}
