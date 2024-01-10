using Common.Attributes;
using DataAccess.Models.PaymentModels;

namespace DataAccess.Services.IRepositories.IPaymentRepositories;

[Service(nameof(IPaymentServiceRepository))]
public interface IPaymentServiceRepository : IBaseModelRepository<PaymentService>
{
}
