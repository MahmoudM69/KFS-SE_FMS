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
    public class EstablishmentRepository : IEstablishmentRepository
    {
        private readonly AppDbContext context;

        public EstablishmentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Establishment> CreateEstablishment(Establishment establishment)
        {
            if(establishment != null)
            {
                if(context.Establishments.FirstOrDefault(x => x.EstablishmentName == establishment.EstablishmentName) == null)
                {
                    var dbEstablishment = await context.Establishments.AddAsync(establishment);
                    if(dbEstablishment.Entity != null)
                    {
                        await context.SaveChangesAsync();
                        return(dbEstablishment.Entity);
                    }
                }

            }
            return null;
        }

        public async Task<Establishment> GetEstablishment(int id)
        {
            if (id > 0)
            {
                Establishment establishment = await context.Establishments.Include(x => x.EstablishmentImages).Include(x => x.EstablishmentType)
                                                                          .Include(x => x.Employees).Include(x => x.FinancialAids)
                                                                          .Include(x => x.Establishment_Products)
                                                                          .FirstOrDefaultAsync(x => x.EstablishmentId == id);
                if (establishment != null)
                {
                    return establishment;
                }
            }
            return null;
        }

        public List<Establishment> GetAllEstablishments()
        {
            List<Establishment> establishments = context.Establishments.Include(x => x.EstablishmentImages).Include(x => x.EstablishmentType)
                                                                                   .Include(x => x.Employees).Include(x => x.FinancialAids)
                                                                                   .Include(x => x.Establishment_Products).ToList();
            if (establishments.Any())
            {
                return(establishments);
            }
            return null;
        }

        public async Task<Establishment> UpdateEstablishment(Establishment inEstablishment)
        {
            if (inEstablishment != null && inEstablishment.EstablishmentId > 0)
            {
                if(await context.Establishments.FindAsync(inEstablishment.EstablishmentId) != null)
                { 
                
                    if (await context.Establishments
                                     .FirstOrDefaultAsync(x => x.EstablishmentName == inEstablishment.EstablishmentName) == null)
                    {
                        Establishment dbEstablishment = context.Establishments.Update(inEstablishment).Entity;
                        if (dbEstablishment != null)
                        {
                            await context.SaveChangesAsync();
                            return(dbEstablishment);
                        }
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
                    context.SaveChanges();
                }
            }
        }
    }
}
