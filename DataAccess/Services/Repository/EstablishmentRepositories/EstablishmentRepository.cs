using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Services.Repository;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;
using DataAccess.Models.EstablishmentModels;

namespace DataAccess.Services.Repository.EstablishmentRepositories;

[Service(nameof(IEstablishmentRepository))]
public class EstablishmentRepository : BaseModelRepository<Establishment>, IEstablishmentRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public EstablishmentRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
