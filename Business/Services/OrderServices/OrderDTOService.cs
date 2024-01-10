using AutoMapper;
using Business.Helpers.Extensions;
using Business.IServices.IOrderServices;
using DTO.DTOs.OrderDTOs;
using FluentValidation;
using Common.Attributes;
using Common.Enums;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using LanguageExt.Common;
using Common.Exceptions;
using DataAccess.Models.OrderModels;
using DataAccess.Services.IRepositories.IOrderRepositories;

namespace Business.Services.OrderServices;

[Service(nameof(IOrderDTOService))]
public class OrderDTOService : BaseDTOService<OrderDTO, Order, IOrderRepository>, IOrderDTOService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderDTO>? _validator;

    public OrderDTOService(IOrderRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public OrderDTOService(IOrderRepository repository, IMapper mapper, IValidator<OrderDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<OrderDTO>>> GetAllCustomerOrders(string customerId, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<OrderDTO, object>>[] includeProperties)
    {
        var (IsValid, ErrorMessage) = customerId.ValidateId<INullable>();
        return IsValid ? await base.FindAsync(x => x.CustomerId == customerId, filterSoftDelete,
            cancellationToken , includeProperties) :
                new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage }));
    }

    public async Task<Result<IEnumerable<OrderDTO>>> GetAllCustomerOrders(string customerId, OrderStatus status, bool filterSoftDelete = true,
        CancellationToken cancellationToken = default, params Expression<Func<OrderDTO, object>>[] includeProperties)
    {
        var (IsValid, ErrorMessage) = customerId.ValidateId<INullable>();
        return IsValid ? await base.FindAsync(x => x.CustomerId == customerId && x.Status == status, filterSoftDelete,
            cancellationToken, includeProperties) :
                new(new BadRequestException(new List<string>() { "The id sent was not valid.", ErrorMessage}));
    }
}
