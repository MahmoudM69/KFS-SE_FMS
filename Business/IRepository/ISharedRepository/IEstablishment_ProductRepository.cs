using Models.DTOModels.ProductDTOs;
using Models.DTOModels.SharedDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.ISharedRepository
{
    public interface IEstablishment_ProductRepository
    {
        public Task<Establishment_ProductDTO> CreateEstablishment_Product(Establishment_ProductDTO establishment_ProductDTO);
        public Task<ICollection<ProductDTO>> GetEstablishmentProducts(int id);
        public Task<Establishment_ProductDTO> UpdateEstablishment_Product(Establishment_ProductDTO establishment_ProductDTO);
        public void DeleteEstablishment_Product();
    }
}
