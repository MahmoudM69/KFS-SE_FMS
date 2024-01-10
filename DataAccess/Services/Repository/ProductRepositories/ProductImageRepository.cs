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
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Services.Repository.SharedRepositories;
using DataAccess.Models.ProductModels;

namespace DataAccess.Services.Repository.ProductRepositories;

[Service(nameof(IProductImageRepository))]
public class ProductImageRepository : BaseImageRepository<ProductImage>, IProductImageRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public ProductImageRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Result<IEnumerable<ProductImage>>> GetProductImages(int productId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<ProductImage, object>>[] includeProperties)
    {
        try
        {
            return await base.FindAsync(x => x.ProductId == productId, filterSoftDelete, cancellationToken, includeProperties);
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occured while trying to get the \"Images\".", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<ProductImage>>> DeleteProductImagesByProductId(int productId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await (await base.FindAsync(x => x.ProductId == productId, filterSoftDelete, cancellationToken)).Match(
                    async succ => await base.DeleteRangeAsync(succ.Select(x => x.Id), cancellationToken),
                    excp => new Task<Result<IEnumerable<ProductImage>>>(() => new Result<IEnumerable<ProductImage>>(excp))
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
