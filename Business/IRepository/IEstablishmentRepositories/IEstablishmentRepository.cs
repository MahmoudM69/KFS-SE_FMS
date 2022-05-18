using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOModels.EstablishmentDTOs;

namespace Business.IRepository.IEstablishmentRepositories
{
    public interface IEstablishmentRepository
    {
        public Task<EstablishmentDTO> CreateEstablishment(EstablishmentDTO establishmentDTO);
        public Task<EstablishmentDTO> GetEstablishment(int id);
        public Task<ICollection<EstablishmentDTO>> GetAllEstablishments();
        public Task<EstablishmentDTO> UpdateEstablishment(int id, EstablishmentDTO establishmentDTO);
        public void DeleteEstablishment(int id);
    }
}
