using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.ISharedRepository
{
    public interface IEstablishment_ProductRepository
    {
        public Task<Establishment_Product> CreateEstablishment_Product(Establishment_Product establishment_Product);
        public Task<List<Product>> GetEstablishmentProducts(int id);
        public Task<List<Establishment_Product>> GetEstablishment_ProductProducts(int id);
        public Task<Establishment_Product> UpdateEstablishment_Product(Establishment_Product establishment_Product);
        public void DeleteEstablishment_Product();
    }
}
