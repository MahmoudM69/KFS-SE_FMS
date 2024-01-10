using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Services.Repository;
using DataAccess.Services.IRepositories.IPaymentRepositories;
using DataAccess.Models.PaymentModels;

namespace DataAccess.Services.Repository.PaymentRepositories;

[Service(nameof(IPaymentServiceRepository))]
public class PaymentServiceRepository : BaseModelRepository<PaymentService>, IPaymentServiceRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public PaymentServiceRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
