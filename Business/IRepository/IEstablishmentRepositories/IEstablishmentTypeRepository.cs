using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOModels.EstablishmentDTOs;

namespace Business.IRepository.IEstablishmentRepositories
{
    public interface IEstablishmentTypeRepository
    {
        public Task<EstablishmentTypeDTO> CreateEstablishmentType(EstablishmentTypeDTO establishmentTypeDTO);
        public Task<EstablishmentTypeDTO> GetEstablishmentType(int Id);
        public Task<ICollection<EstablishmentTypeDTO>> GetAllEstablishmentsType();
        public Task<EstablishmentTypeDTO> UpdateEstablishmentType(int id, EstablishmentTypeDTO establishmentTypeDTO);
        public void DeleteEstablishmentType(int Id);
    }
}
