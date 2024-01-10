using AutoMapper;
using Business.IServices.IPaymentServices;
using DTO.DTOs.PaymentDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Models.PaymentModels;
using DataAccess.Services.IRepositories.IPaymentRepositories;

namespace Business.Services.PaymentServices;

[Service(nameof(IPaymentDTOService))]
public class PaymentDTOService : BaseDTOService<PaymentDTO, Payment, IPaymentRepository>, IPaymentDTOService
{
    private readonly IPaymentRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<PaymentDTO>? _validator;

    public PaymentDTOService(IPaymentRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public PaymentDTOService(IPaymentRepository repository, IMapper mapper, IValidator<PaymentDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
}
