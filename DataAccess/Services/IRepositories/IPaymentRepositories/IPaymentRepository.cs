using Common.Attributes;
using DataAccess.Services.IRepositories;
using DataAccess.Models.PaymentModels;

namespace DataAccess.Services.IRepositories.IPaymentRepositories;

[Service(nameof(IPaymentRepository))]
public interface IPaymentRepository : IBaseModelRepository<Payment>
{
}
