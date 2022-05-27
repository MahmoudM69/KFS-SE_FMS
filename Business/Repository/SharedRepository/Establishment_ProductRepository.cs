using AutoMapper;
using Business.IRepository.IEstablishmentRepositories;
using Business.IRepository.IProductRepositories;
using Business.IRepository.ISharedRepository;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using Models.DTOModels.EstablishmentDTOs;
using Models.DTOModels.ProductDTOs;
using Models.DTOModels.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.SharedRepository
{
    public class Establishment_ProductRepository : IEstablishment_ProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IEstablishmentRepository establishmentRepository;
        private readonly IProductRepository productRepository;
        public Establishment_ProductRepository(AppDbContext context, IMapper mapper, IEstablishmentRepository establishmentRepository, IProductRepository productRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.establishmentRepository = establishmentRepository;
            this.productRepository = productRepository;
        }
        public async Task<Establishment_ProductDTO> CreateEstablishment_Product(Establishment_ProductDTO establishment_ProductDTO)
        {
            if(establishment_ProductDTO != null &&
               establishment_ProductDTO.ProductId > 0 &&
               establishment_ProductDTO.EstablishmentId > 0)
            {
                Establishment_Product establishment_Product = mapper.Map<Establishment_Product>(establishment_ProductDTO);
                establishment_Product.Establishment = await context.Establishments.FindAsync(establishment_Product.EstablishmentId);
                establishment_Product.Product = await context.Products.FindAsync(establishment_Product.ProductId);
                var addedEstablishment_Product = await context.Establishment_Products.AddAsync(establishment_Product);
                await context.SaveChangesAsync();
                Establishment_ProductDTO returnEstablishment_ProductDTO = mapper.Map<Establishment_ProductDTO>(addedEstablishment_Product.Entity);
                return returnEstablishment_ProductDTO;
                //establishment_ProductDTO.EstablishmentDTO = await establishmentRepository.GetEstablishment(establishment_ProductDTO.EstablishmentDTOId);
                //if (establishment_ProductDTO.EstablishmentDTO != null)
                //{
                //    //var addedProduct = await context.Products.AddAsync(mapper.Map<Product>(establishment_ProductDTO.ProductDTO));

                //}
            }
            return null;
        }

        public Task<ICollection<ProductDTO>> GetEstablishmentProducts(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Establishment_ProductDTO> UpdateEstablishment_Product(Establishment_ProductDTO establishment_ProductDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteEstablishment_Product()
        {
            throw new NotImplementedException();
        }
    }
}
