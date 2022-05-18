using System.Threading.Tasks;
using System.Collections.Generic;
using Models.DTOModels.EstablishmentDTOs;

namespace Business.IRepository.IEstablishmentRepositories
{
    public interface IEstablishmentImageRepository
    {
        public Task<EstablishmentImageDTO> CreateEstablishment(EstablishmentImageDTO establishmentImageDTO);
        public Task<EstablishmentImageDTO> GetEstablishment(int Id);
        public void DeleteEstablishment(int Id);
        //public Task<ICollection<EstablishmentImageDTO>> GetAllEstablishments();
        //public Task<EstablishmentImageDTO> UpdateEstablishment(EstablishmentImageDTO establishmentImageDTO);
    }
}
