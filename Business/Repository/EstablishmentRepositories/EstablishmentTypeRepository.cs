using AutoMapper;
using Business.IRepository.IEstablishmentRepositories;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using Microsoft.EntityFrameworkCore;
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

        public EstablishmentTypeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<EstablishmentType> CreateEstablishmentType(EstablishmentType establishmentType)
        {
            if(establishmentType != null)
            {
                if(await context.EstablishmentTypes.FirstOrDefaultAsync(x => x.Type == establishmentType.Type) == null)
                {
                    var dbEstablishmentType = await context.EstablishmentTypes.AddAsync(establishmentType);
                    if(dbEstablishmentType.Entity != null)
                    {
                        await context.SaveChangesAsync();
                        return(dbEstablishmentType.Entity);
                    }
                }
            }
            return null;
        }

        public async Task<EstablishmentType> GetEstablishmentType(int id)
        {
            if(id.ToString() != null)
            {
                EstablishmentType establishmentType = await context.EstablishmentTypes.Include(x => x.Establishments)
                                                                                      .FirstOrDefaultAsync(x => x.EstablishmentTypeId == id);
                if(establishmentType != null)
                {
                    return(establishmentType);
                }
            }
            return null;
        }
     
        public async Task<List<Establishment>> GetTypeEstablishments(int id)
        {
            if(id > 0)
            {
                EstablishmentType establishmentTypes = await context.EstablishmentTypes.Include(x => x.Establishments).FirstOrDefaultAsync(x => x.EstablishmentTypeId == id);
                if(establishmentTypes != null)
                {
                    if (establishmentTypes.Establishments.Any())
                    {
                        return (establishmentTypes.Establishments);
                    }
                }
            }
            return null;
        }

        public async Task<List<EstablishmentType>> GetAllTypes()
        {
            List<EstablishmentType> establishmentTypes = await context.EstablishmentTypes.Include(x => x.Establishments).ToListAsync();
            if (establishmentTypes.Any())
            {
                return (establishmentTypes);
            }
            return null;
        }

        public async Task<EstablishmentType> UpdateEstablishmentType(EstablishmentType establishmentType)
        {
            if (establishmentType.EstablishmentTypeId > 0 && establishmentType != null)
            {
                if(await context.EstablishmentTypes.FirstOrDefaultAsync(x => x.EstablishmentTypeId == establishmentType.EstablishmentTypeId) != null)
                {
                    if (context.EstablishmentTypes.Where(x => x.Type == establishmentType.Type &&
                                                        x.Type != establishmentType.Type) == null)
                    {
                        EstablishmentType dbEstablishmentType = context.EstablishmentTypes.Update(establishmentType).Entity;
                        if (dbEstablishmentType != null)
                        {
                            await context.SaveChangesAsync();
                            return(dbEstablishmentType);
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
                    context.SaveChanges();
                }
            }
        }

    }
}
