using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using Common.Enums;
using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using System.Linq.Expressions;
using DataAccess.Services.Repository;
using DataAccess.Models.OrderModels;
using DataAccess.Services.IRepositories.IOrderRepositories;

namespace DataAccess.Services.Repository.OrderRepositories;

[Service(nameof(IOrderRepository))]
public class OrderRepository : BaseModelRepository<Order>, IOrderRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public OrderRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Result<IEnumerable<Order>>> GetAllCustomerOrders(string customerId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Order, object>>[] includeProperties)
    {
        try
        {
            return await base.FindAsync(x => x.CustomerId == customerId, filterSoftDelete, cancellationToken, includeProperties);
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occured while trying to get the \"Orders\".", ex.Message
            }));
        }
    }

    public async Task<Result<IEnumerable<Order>>> GetAllCustomerOrders(string customerId, OrderStatus status, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Order, object>>[] includeProperties)
    {
        try
        {
            return await base.FindAsync(x => x.CustomerId == customerId && x.Status == status, filterSoftDelete,
                cancellationToken, includeProperties);
        }
        catch (Exception ex)
        {
            return new(new ServerException(new List<string>() {
                $"An error occured while trying to get the \"Orders\".", ex.Message
            }));
        }
    }
}
