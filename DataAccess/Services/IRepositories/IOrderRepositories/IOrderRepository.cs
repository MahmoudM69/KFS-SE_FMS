using Common.Attributes;
using Common.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using LanguageExt.Common;
using DataAccess.Models.OrderModels;
using DataAccess.Services.IRepositories;

namespace DataAccess.Services.IRepositories.IOrderRepositories;

[Service(nameof(IOrderRepository))]
public interface IOrderRepository : IBaseModelRepository<Order>
{
    public Task<Result<IEnumerable<Order>>> GetAllCustomerOrders(string customerId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Order, object>>[] includeProperties);
    public Task<Result<IEnumerable<Order>>> GetAllCustomerOrders(string customerId, OrderStatus status, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<Order, object>>[] includeProperties);
}
