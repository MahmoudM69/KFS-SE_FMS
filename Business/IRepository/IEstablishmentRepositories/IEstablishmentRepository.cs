using System.Collections.Generic;
using System.Threading.Tasks;
using DataAcesss.Data.EstablishmentModels;

namespace Business.IRepository.IEstablishmentRepositories
{
    public interface IEstablishmentRepository
    {
        public Task<Establishment> CreateEstablishment(Establishment establishment);
        public Task<Establishment> GetEstablishment(int id);
        public Task<List<Establishment>> GetAllEstablishments();
        public Task<Establishment> UpdateEstablishment(Establishment establishment);
        public void DeleteEstablishment(int id);
    }
}
