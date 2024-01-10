using Common.Attributes;
using DataAccess.Services.IRepositories;
using DataAccess.Models.EstablishmentModels;

namespace DataAccess.Services.IRepositories.IEstablishmentRepositories;

[Service(nameof(IEstablishmentRepository))]
public interface IEstablishmentRepository : IBaseModelRepository<Establishment>
{
}
