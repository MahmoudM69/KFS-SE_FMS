using Microsoft.EntityFrameworkCore;
using Common.Attributes;
using DataAccess.Models.EstablishmentModels;
using DataAccess;
using DataAccess.Services.IRepositories.IEstablishmentRepositories;
using DataAccess.Services.Repository.SharedRepositories;

namespace DataAccess.Services.Repository.EstablishmentRepositories;

[Service(nameof(IEstablishmentTypeRepository))]
public class EstablishmentTypeRepository : BaseTypeRepository<EstablishmentType>, IEstablishmentTypeRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public EstablishmentTypeRepository(IDbContextFactory<AppDbContext> contextFactory) : base(contextFactory)
    {
        _contextFactory = contextFactory;
    }
}
