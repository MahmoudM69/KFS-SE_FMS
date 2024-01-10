using Common.Attributes;
using DataAccess.Models.SharedModels;

namespace DataAccess.Services.IRepositories.ISharedRepositories;

[Service(nameof(IEstablishment_ProductRepository))]
public interface IEstablishment_ProductRepository : IBaseModelRepository<Establishment_Product>
{
}
