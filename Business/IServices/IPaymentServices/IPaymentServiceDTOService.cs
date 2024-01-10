using DTO.DTOs.PaymentDTOs;
using Common.Attributes;
using DataAccess.Services.IRepositories.IPaymentRepositories;
using DataAccess.Models.PaymentModels;

namespace Business.IServices.IPaymentServices;

[Service(nameof(IPaymentServiceDTOService))]
public interface IPaymentServiceDTOService : IBaseDTOService<PaymentServiceDTO, PaymentService, IPaymentServiceRepository>
{
}
