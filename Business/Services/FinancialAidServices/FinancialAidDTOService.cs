using AutoMapper;
using Business.IServices.IFinancialAidServices;
using DTO.DTOs.FinancialAidDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Services.IRepositories.IFinancialAidRepositories;

namespace Business.Services.FinancialAidServices;

[Service(nameof(IFinancialAidDTOService))]
public class FinancialAidDTOService :
    BaseDTOService<FinancialAidDTO, FinancialAid, IFinancialAidRepository>, IFinancialAidDTOService
{
    private readonly IFinancialAidRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<FinancialAidDTO>? _validator;

    public FinancialAidDTOService(IFinancialAidRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public FinancialAidDTOService(IFinancialAidRepository repository, IMapper mapper, IValidator<FinancialAidDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
}
