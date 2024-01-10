using AutoMapper;
using Business.IServices.ISharedServices;
using DTO.DTOs.SharedDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Models.SharedModels;
using DataAccess.Services.IRepositories.ISharedRepositories;

namespace Business.Services.SharedServices;

[Service(nameof(IProductType_FinancialAidDTOService))]
public class ProductType_FinancialAidDTOService :
    BaseDTOService<ProductType_FinancialAidDTO, ProductType_FinancialAid, IProductType_FinancialAidRepository>,
    IProductType_FinancialAidDTOService
{
    private readonly IProductType_FinancialAidRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductType_FinancialAidDTO>? _validator;

    public ProductType_FinancialAidDTOService(IProductType_FinancialAidRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public ProductType_FinancialAidDTOService(
        IProductType_FinancialAidRepository repository, IMapper mapper, IValidator<ProductType_FinancialAidDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
}
