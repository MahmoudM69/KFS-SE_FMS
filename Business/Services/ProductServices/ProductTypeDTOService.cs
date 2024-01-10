using AutoMapper;
using Business.IServices.IProductServices;
using DTO.DTOs.ProductDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Models.ProductModels;

namespace Business.Services.ProductServices;

[Service(nameof(IProductTypeDTOService))]
public class ProductTypeDTOService : BaseDTOService<ProductTypeDTO, ProductType, IProductTypeRepository>, IProductTypeDTOService
{
    private readonly IProductTypeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductTypeDTO>? _validator;

    public ProductTypeDTOService(IProductTypeRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public ProductTypeDTOService(IProductTypeRepository repository, IMapper mapper, IValidator<ProductTypeDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
}
