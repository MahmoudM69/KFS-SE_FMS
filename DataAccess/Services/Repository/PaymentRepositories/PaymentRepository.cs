using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Services.Repository;
using DataAccess.Models.PaymentModels;
using DataAccess.Services.IRepositories.IPaymentRepositories;

namespace DataAccess.Services.Repository.PaymentRepositories;

[Service(nameof(IPaymentRepository))]
public class PaymentRepository : BaseModelRepository<Payment>, IPaymentRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public PaymentRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
