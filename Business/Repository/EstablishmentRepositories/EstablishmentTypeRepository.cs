using AutoMapper;
using Business.IRepository.IEstablishmentRepositories;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using Microsoft.EntityFrameworkCore;
using Models.DTOModels.EstablishmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.EstablishmentRepositories
{
    public class EstablishmentTypeRepository : IEstablishmentTypeRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public EstablishmentTypeRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<EstablishmentTypeDTO> CreateEstablishmentType(EstablishmentTypeDTO establishmentTypeDTO)
        {
            if(establishmentTypeDTO != null)
            {
                if(context.EstablishmentTypes.FirstOrDefault(x => x.Type == establishmentTypeDTO.Type) == null)
                {
                    EstablishmentType establishmentType = mapper.Map<EstablishmentType>(establishmentTypeDTO);
                    var dbEstablishmentType = await context.EstablishmentTypes.AddAsync(establishmentType);
                    if(dbEstablishmentType != null)
                    {
                        await context.SaveChangesAsync();
                        return mapper.Map<EstablishmentTypeDTO>(dbEstablishmentType);
                    }
                }
            }
            return null;
        }

        public async Task<EstablishmentTypeDTO> GetEstablishmentType(int id)
        {
            if(id.ToString() != null)
            {
                EstablishmentType establishmentType = await context.EstablishmentTypes.Include(x => x.Establishments)
                                                                                      .FirstOrDefaultAsync(x => x.EstablishmentTypeId == id);
                if(establishmentType != null)
                {
                    return mapper.Map<EstablishmentTypeDTO>(establishmentType);
                }
            }
            return null;
        }

        public async Task<ICollection<EstablishmentTypeDTO>> GetAllEstablishmentsType()
        {
            ICollection<EstablishmentType> establishmentTypes = await context.EstablishmentTypes.Include(x => x.Establishments).ToListAsync();
            if (establishmentTypes.Any())
            {
                return mapper.Map< ICollection<EstablishmentTypeDTO>>(establishmentTypes);
            }
            return null;
        }

        public async Task<EstablishmentTypeDTO> UpdateEstablishmentType(int id, EstablishmentTypeDTO establishmentTypeDTO)
        {
            if (id.ToString() != null && establishmentTypeDTO != null)
            {
                EstablishmentType establishmentType = await context.EstablishmentTypes.Include(x => x.Establishments)
                                                                                      .FirstOrDefaultAsync(x => x.EstablishmentTypeId == id);
                if (establishmentType != null)
                {
                    if(context.EstablishmentTypes.Where(x => x.Type == establishmentTypeDTO.Type &&
                                                     x.Type != establishmentType.Type) == null)
                    {
                        establishmentTypeDTO.EstablishmentDTOs ??= mapper.Map<ICollection<EstablishmentDTO>>(establishmentType.Establishments);
                        establishmentType = mapper.Map<EstablishmentTypeDTO, EstablishmentType>(establishmentTypeDTO, establishmentType);
                        EstablishmentType dbEstablishmentType = context.EstablishmentTypes.Update(establishmentType).Entity;
                        if (dbEstablishmentType != null)
                        {
                            await context.SaveChangesAsync();
                            return mapper.Map<EstablishmentTypeDTO>(dbEstablishmentType);
                        }
                    }
                }
            }
            return null;
        }

        public async void DeleteEstablishmentType(int id)
        {
            if (id.ToString() != null)
            {
                EstablishmentType establishmentType = await context.EstablishmentTypes.Include(x => x.Establishments)
                                                                                      .FirstOrDefaultAsync(x => x.EstablishmentTypeId == id);
                if (establishmentType != null)
                {
                    context.EstablishmentTypes.Remove(establishmentType);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
