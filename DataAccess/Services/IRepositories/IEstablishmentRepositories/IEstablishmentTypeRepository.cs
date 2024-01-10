using Common.Attributes;
using DataAccess.Models.EstablishmentModels;
using DataAccess.Services.IRepositories;

namespace DataAccess.Services.IRepositories.IEstablishmentRepositories;

[Service(nameof(IEstablishmentTypeRepository))]
public interface IEstablishmentTypeRepository : IBaseModelRepository<EstablishmentType>
{
}
