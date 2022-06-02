using DataAcesss.Data.FinancialAidModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IFinancialAidRepositories
{
    public interface IFinancialAidRepository
    {
        public Task<FinancialAid> CreateFinancialAid(FinancialAid financialAid);
        public Task<FinancialAid> GetFinancialAid(int id);
        public List<FinancialAid> GetAllFinancialAids();
        public Task<List<FinancialAid>> GetEstablishmentFinancialAids(int id);
        public List<FinancialAid> GetProductTypeFinancialAids(int id);
        public Task<FinancialAid> UpdateFinancialAid(int id, FinancialAid financialAid);
        public void DeleteFinancialAid(int id);
    }
}
