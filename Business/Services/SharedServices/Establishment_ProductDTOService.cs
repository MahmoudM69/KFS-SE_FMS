using AutoMapper;
using Business.IServices.ISharedServices;
using DTO.DTOs.SharedDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Services.IRepositories.ISharedRepositories;
using DataAccess.Models.SharedModels;

namespace Business.Services.SharedServices;

[Service(nameof(IEstablishment_ProductDTOService))]
public class Establishment_ProductDTOService : 
    BaseDTOService<Establishment_ProductDTO, Establishment_Product, IEstablishment_ProductRepository>,
    IEstablishment_ProductDTOService
{
    private readonly IEstablishment_ProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<Establishment_ProductDTO>? _validator;

    public Establishment_ProductDTOService(IEstablishment_ProductRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public Establishment_ProductDTOService(
        IEstablishment_ProductRepository repository, IMapper mapper, IValidator<Establishment_ProductDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
}
