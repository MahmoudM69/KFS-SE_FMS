using AutoMapper;
using Business.IServices.IEstablishmentServices;
using DTO.DTOs.EstablishmentDTOs;
using FluentValidation;
using Common.Attributes;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;
using DataAccess.Models.EstablishmentModels;

namespace Business.Services.EstablishmentServices;

[Service(nameof(IEstablishmentDTOService))]
public class EstablishmentDTOService : BaseDTOService<EstablishmentDTO, Establishment, IEstablishmentRepository>, IEstablishmentDTOService
{
    private readonly IEstablishmentRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<EstablishmentDTO>? _validator;

    public EstablishmentDTOService(IEstablishmentRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public EstablishmentDTOService(IEstablishmentRepository repository, IMapper mapper, IValidator<EstablishmentDTO> validator) :
        base(repository, mapper, validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }
}
