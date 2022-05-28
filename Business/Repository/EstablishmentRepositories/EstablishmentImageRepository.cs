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
    public class EstablishmentImageRepository : IEstablishmentImageRepository
    {
        private readonly AppDbContext context;

        public EstablishmentImageRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<EstablishmentImage> CreateEstablishment(EstablishmentImage establishmentImage)
        {
            if(establishmentImage != null && establishmentImage.Establishment != null)
            {
                var dbEstablishmentImage = await context.EstablishmentImages.AddAsync(establishmentImage);
                if(dbEstablishmentImage != null)
                {
                    await context.SaveChangesAsync();
                    return (dbEstablishmentImage.Entity);
                }
            }
            return null;
        }

        public async Task<EstablishmentImage> GetEstablishment(int id)
        {
            if(id.ToString() != null)
            {
                EstablishmentImage establishmentImage = await context.EstablishmentImages.Include(x => x.Establishment)
                                                                                         .FirstOrDefaultAsync(x => x.EstablishmentImageId == id);
                if(establishmentImage != null)
                {
                    return(establishmentImage);
                }
            }
            return null;
        }

        public async void DeleteEstablishment(int id)
        {
            if(id.ToString() != null)
            {
                EstablishmentImage establishmentImage = await context.EstablishmentImages.FirstOrDefaultAsync(x => x.EstablishmentImageId == id);
                if(establishmentImage != null)
                {
                    context.EstablishmentImages.Remove(establishmentImage);
                    context.SaveChanges();
                }
            }
        }
    }
}
