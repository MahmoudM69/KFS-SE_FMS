using DTO.DTOs.PaymentDTOs;
using Common.Attributes;
using DataAccess.Models.PaymentModels;
using DataAccess.Services.IRepositories.IPaymentRepositories;

namespace Business.IServices.IPaymentServices;

[Service(nameof(IPaymentDTOService))]
public interface IPaymentDTOService : IBaseDTOService<PaymentDTO, Payment, IPaymentRepository>
{
}
