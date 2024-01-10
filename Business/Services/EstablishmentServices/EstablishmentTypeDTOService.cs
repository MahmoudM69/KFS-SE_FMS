using AutoMapper;
using Business.IServices.IEstablishmentServices;
using DTO.DTOs.EstablishmentDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;

namespace Business.Services.EstablishmentServices;

[Service(nameof(IEstablishmentTypeDTOService))]
public class EstablishmentTypeDTOService :
    BaseDTOService<EstablishmentTypeDTO, EstablishmentType, IEstablishmentTypeRepository>, IEstablishmentTypeDTOService
{
    private readonly IEstablishmentTypeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<EstablishmentTypeDTO>? _validator;

    public EstablishmentTypeDTOService(IEstablishmentTypeRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public EstablishmentTypeDTOService(
        IEstablishmentTypeRepository repository, IMapper mapper, IValidator<EstablishmentTypeDTO> validator) : 
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
}
