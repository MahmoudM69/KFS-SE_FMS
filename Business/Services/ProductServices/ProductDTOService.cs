using AutoMapper;
using Business.IServices.IProductServices;
using DTO.DTOs.ProductDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Services.IRepositories.IProductRepositories;
using DataAccess.Models.ProductModels;

namespace Business.Services.ProductServices;

[Service(nameof(IProductDTOService))]
public class ProductDTOService : BaseDTOService<ProductDTO, Product, IProductRepository>, IProductDTOService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductDTO>? _validator;

    public ProductDTOService(IProductRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public ProductDTOService(IProductRepository repository, IMapper mapper, IValidator<ProductDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
}
