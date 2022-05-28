using AutoMapper;
using Business.IRepository.IEstablishmentRepositories;
using Business.IRepository.IProductRepositories;
using Business.IRepository.ISharedRepository;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using Microsoft.EntityFrameworkCore;
using Models.DTOModels.EstablishmentDTOs;
using Models.DTOModels.OrderDTOs;
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

        public async Task<ICollection<Establishment_ProductDTO>> GetEstablishment_ProductProducts(int id)
        {
            if(id.ToString() != null && id > 0)
            {
                List<Establishment_Product> establishment_Products = await context.Establishment_Products.Include(x => x.Establishment)
                                                                                                               .Include(x => x.Product)
                                                                                                               .Include(x => x.Orders)
                                                                                                               .Where(x => x.EstablishmentId == id).ToListAsync();
                List<Establishment_ProductDTO> establishment_ProductDTOs = mapper.Map<List<Establishment_ProductDTO>>(establishment_Products);
                for(int i = 0; i < establishment_ProductDTOs.Count(); i++)
                {
                    establishment_ProductDTOs[i].ProductDTO = mapper.Map<ProductDTO>(establishment_Products[i].Product);
                    establishment_ProductDTOs[i].EstablishmentDTO = mapper.Map<EstablishmentDTO>(establishment_Products[i].Establishment);
                    establishment_ProductDTOs[i].OrderDTOs = mapper.Map<List<OrderDTO>>(establishment_Products[i].Orders);
                    establishment_ProductDTOs[i].ProductDTO = await productRepository.GetProduct(establishment_ProductDTOs[i].ProductId);
                    establishment_ProductDTOs[i].EstablishmentDTO = await establishmentRepository.GetEstablishment(establishment_ProductDTOs[i].EstablishmentId);
                }
                return establishment_ProductDTOs;
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
