using AutoMapper;
using Business.IRepository.IEstablishmentRepositories;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using Microsoft.EntityFrameworkCore;
using Models.DTOModels.EmpolyeeDTOs;
using Models.DTOModels.EstablishmentDTOs;
using Models.DTOModels.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.EstablishmentRepositories
{
    public class EstablishmentRepository : IEstablishmentRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public EstablishmentRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<EstablishmentDTO> CreateEstablishment(EstablishmentDTO establishmentDTO)
        {
            if(establishmentDTO != null)
            {
                if(context.Establishments.FirstOrDefault(x => x.EstablishmentName == establishmentDTO.EstablishmentName) == null)
                {
                    Establishment establishment = mapper.Map<Establishment>(establishmentDTO);
                    var dbEstablishment = await context.Establishments.AddAsync(establishment);
                    if(dbEstablishment != null)
                    {
                        await context.SaveChangesAsync();
                        return mapper.Map<EstablishmentDTO>(dbEstablishment);
                    }
                }

            }
            return null;
        }

        public async Task<EstablishmentDTO> GetEstablishment(int id)
        {
            if (id > 0)
            {
                Establishment establishment = await context.Establishments.Include(x => x.EstablishmentImages).Include(x => x.EstablishmentType)
                                                                          .Include(x => x.Employees).Include(x => x.FinancialAids)
                                                                          .Include(x => x.Establishment_Products)
                                                                          .FirstOrDefaultAsync(x => x.EstablishmentId == id);
                if(establishment != null)
                {
                    return mapper.Map<EstablishmentDTO>(establishment);
                }
            }
            return null;
        }

        public async Task<ICollection<EstablishmentDTO>> GetAllEstablishments()
        {
            ICollection<Establishment> establishments = await context.Establishments.Include(x => x.EstablishmentImages).Include(x => x.EstablishmentType)
                                                                                   .Include(x => x.Employees).Include(x => x.FinancialAids)
                                                                                   .Include(x => x.Establishment_Products).ToListAsync();
            if (establishments.Any())
            {
                return mapper.Map<ICollection<EstablishmentDTO>>(establishments);
            }
            return null;
        }

        public async Task<EstablishmentDTO> UpdateEstablishment(int id, EstablishmentDTO establishmentDTO)
        {
            if (id.ToString() != null && establishmentDTO != null)
            {
                Establishment establishment = await context.Establishments.Include(x => x.EstablishmentImages).Include(x => x.EstablishmentType)
                                                                          .Include(x => x.Employees).Include(x => x.FinancialAids)
                                                                          .Include(x => x.Establishment_Products).FirstOrDefaultAsync(x => x.EstablishmentId == id);

                if (context.Establishments.Where(x => x.EstablishmentName == establishmentDTO.EstablishmentName && 
                                                 x.EstablishmentName != establishment.EstablishmentName) == null)
                {
                    establishmentDTO.EmployeeDTOs ??= mapper.Map<ICollection<EmployeeDTO>>(establishment.Employees);
                    establishmentDTO.EstablishmentImageDTOs ??= mapper.Map<ICollection<EstablishmentImageDTO>>(establishment.EstablishmentImages);
                    establishmentDTO.Establishment_ProductDTOs ??= mapper.Map<ICollection<Establishment_ProductDTO>>(establishment.Establishment_Products);
                    establishmentDTO.EstablishmentTypeDTO ??= mapper.Map<EstablishmentTypeDTO>(establishment.EstablishmentType);
                    establishment = mapper.Map<EstablishmentDTO, Establishment>(establishmentDTO, establishment);
                    Establishment dbEstablishment = context.Establishments.Update(establishment).Entity;
                    if (dbEstablishment != null)
                    {
                        await context.SaveChangesAsync();
                        return mapper.Map<EstablishmentDTO>(dbEstablishment);
                    }
                }
            }
            return null;
        }

        public async void DeleteEstablishment(int id)
        {
            if(id.ToString() != null)
            {
                Establishment establishment = await context.Establishments.FindAsync(id);
                if(establishment != null)
                {
                    context.Establishments.Remove(establishment);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
