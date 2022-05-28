using AutoMapper;
using Business.IRepository.IEstablishmentRepositories;
using Business.IRepository.IProductRepositories;
using Business.IRepository.ISharedRepository;
using DataAcesss.Data;
using DataAcesss.Data.EstablishmentModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using Microsoft.EntityFrameworkCore;
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
        private readonly IEstablishmentRepository establishmentRepository;
        private readonly IProductRepository productRepository;
        public Establishment_ProductRepository(AppDbContext context, IEstablishmentRepository establishmentRepository, IProductRepository productRepository)
        {
            this.context = context;
            this.establishmentRepository = establishmentRepository;
            this.productRepository = productRepository;
        }
        public async Task<Establishment_Product> CreateEstablishment_Product(Establishment_Product establishment_Product)
        {
            if(establishment_Product != null &&
               establishment_Product.ProductId > 0 &&
               establishment_Product.EstablishmentId > 0)
            {
                var addedEstablishment_Product = await context.Establishment_Products.AddAsync(establishment_Product);
                if(addedEstablishment_Product != null)
                {
                    await context.SaveChangesAsync();
                    return addedEstablishment_Product.Entity;
                }
            }
            return null;
        }

        public async Task<List<Establishment_Product>> GetEstablishment_ProductProducts(int id)
        {
            if(id > 0)
            {
                List<Establishment_Product> establishment_Products = await context.Establishment_Products.Include(x => x.Establishment)
                                                                                                               .Include(x => x.Product).ThenInclude(p => p.ProductImages)
                                                                                                               .Include(x => x.Orders)
                                                                                                               .Where(x => x.EstablishmentId == id).ToListAsync();
                if(establishment_Products.Any())
                    return establishment_Products;
            }
            return null;
        }

        public Task<List<Product>> GetEstablishmentProducts(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Establishment_Product> UpdateEstablishment_Product(Establishment_Product establishment_Product)
        {
            if (establishment_Product != null && 
                establishment_Product.Id > 0 && 
                establishment_Product.Product != null && 
                establishment_Product.Establishment != null)
            {
                var updatedEstablishment_Product = context.Establishment_Products.Update(establishment_Product);
                await context.SaveChangesAsync();
                return updatedEstablishment_Product.Entity;
            }
            return null;
        }

        public void DeleteEstablishment_Product()
        {
            throw new NotImplementedException();
        }
    }
}
