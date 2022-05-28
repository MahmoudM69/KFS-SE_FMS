using DataAcesss.Data.EstablishmentModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.IRepository.IEstablishmentRepositories
{
    public interface IEstablishmentTypeRepository
    {
        public Task<EstablishmentType> CreateEstablishmentType(EstablishmentType establishmentType);
        public Task<EstablishmentType> GetEstablishmentType(int Id);
        public Task<List<Establishment>> GetTypeEstablishments(int id);
        public Task<List<EstablishmentType>> GetAllTypes();
        public Task<EstablishmentType> UpdateEstablishmentType(EstablishmentType establishmentType);
        public void DeleteEstablishmentType(int Id);
    }
}
