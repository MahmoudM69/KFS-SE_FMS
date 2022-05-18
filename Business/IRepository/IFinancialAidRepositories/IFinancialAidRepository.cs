using Models.DTOModels.FinancialAidDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IFinancialAidRepositories
{
    public interface IFinancialAidRepository
    {
        public Task<FinancialAidDTO> CreateFinancialAid(FinancialAidDTO financialAidDTO);
        public Task<FinancialAidDTO> GetFinancialAid(int id);
        public Task<ICollection<FinancialAidDTO>> GetAllFinancialAids();
        public Task<ICollection<FinancialAidDTO>> GetEstablishmentFinancialAids(int id);
        public Task<ICollection<FinancialAidDTO>> GetProductTypeFinancialAids(int id);
        public Task<FinancialAidDTO> UpdateFinancialAid(int id, FinancialAidDTO financialAidDTO);
        public void DeleteFinancialAid(int id);
    }
}
