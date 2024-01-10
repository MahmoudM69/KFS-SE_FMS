using DTO.DTOs.OrderDTOs;
using Common.Attributes;
using Common.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using System.Linq.Expressions;
using System;
using DataAccess.Models.OrderModels;
using DataAccess.Services.IRepositories.IOrderRepositories;

namespace Business.IServices.IOrderServices;

[Service(nameof(IOrderDTOService))]
public interface IOrderDTOService : IBaseDTOService<OrderDTO, Order, IOrderRepository>
{
    public Task<Result<IEnumerable<OrderDTO>>> GetAllCustomerOrders(string customerId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<OrderDTO, object>>[] includeProperties);
    public Task<Result<IEnumerable<OrderDTO>>> GetAllCustomerOrders(string customerId, OrderStatus status, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<OrderDTO, object>>[] includeProperties);
}
