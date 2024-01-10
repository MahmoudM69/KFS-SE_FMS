using DTO.DTOs.FinancialAidDTOs;
using Common.Attributes;
using DataAccess.Models.FinancialAidModels;
using DataAccess.Services.IRepositories.IFinancialAidRepositories;

namespace Business.IServices.IFinancialAidServices;

[Service(nameof(IFinancialAidDTOService))]
public interface IFinancialAidDTOService : IBaseDTOService<FinancialAidDTO, FinancialAid, IFinancialAidRepository>
{
}
