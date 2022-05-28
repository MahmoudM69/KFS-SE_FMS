using System.Threading.Tasks;
using System.Collections.Generic;
using DataAcesss.Data.EstablishmentModels;

namespace Business.IRepository.IEstablishmentRepositories
{
    public interface IEstablishmentImageRepository
    {
        public Task<EstablishmentImage> CreateEstablishment(EstablishmentImage establishmentImage);
        public Task<EstablishmentImage> GetEstablishment(int Id);
        public void DeleteEstablishment(int Id);
        //public Task<List<EstablishmentImage>> GetAllEstablishments();
        //public Task<EstablishmentImage> UpdateEstablishment(EstablishmentImage establishmentImage);
    }
}
