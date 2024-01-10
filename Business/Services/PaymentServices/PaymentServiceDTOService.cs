using AutoMapper;
using Business.IServices.IPaymentServices;
using DTO.DTOs.PaymentDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Services.IRepositories.IPaymentRepositories;
using DataAccess.Models.PaymentModels;

namespace Business.Services.PaymentServices;

[Service(nameof(IPaymentServiceDTOService))]
public class PaymentServiceDTOService :
    BaseDTOService<PaymentServiceDTO, PaymentService, IPaymentServiceRepository>, IPaymentServiceDTOService
{
    private readonly IPaymentServiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<PaymentServiceDTO>? _validator;

    public PaymentServiceDTOService(IPaymentServiceRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    //public PaymentServiceDTOService(IPaymentServiceRepository repository, IMapper mapper, IValidator<PaymentServiceDTO> validator) :
    //    base(repository, mapper, validator)
    //{
    //    _repository = repository;
    //    _mapper = mapper;
    //    _validator = validator;
    //}
}
