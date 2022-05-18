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
    public class EstablishmentImageRepository : IEstablishmentImageRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public EstablishmentImageRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<EstablishmentImageDTO> CreateEstablishment(EstablishmentImageDTO establishmentImageDTO)
        {
            if(establishmentImageDTO != null && establishmentImageDTO.EstablishmentDTO != null)
            {
                EstablishmentImage establishmentImage = mapper.Map<EstablishmentImage>(establishmentImageDTO);
                var dbEstablishmentImage = await context.EstablishmentImages.AddAsync(establishmentImage);
                if(dbEstablishmentImage != null)
                {
                    await context.SaveChangesAsync();
                    return mapper.Map<EstablishmentImageDTO>(dbEstablishmentImage);
                }
            }
            return null;
        }

        public async Task<EstablishmentImageDTO> GetEstablishment(int id)
        {
            if(id.ToString() != null)
            {
                EstablishmentImage establishmentImage = await context.EstablishmentImages.Include(x => x.Establishment)
                                                                                         .FirstOrDefaultAsync(x => x.EstablishmentImageId == id);
                if(establishmentImage != null)
                {
                    return mapper.Map<EstablishmentImageDTO>(establishmentImage);
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
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
