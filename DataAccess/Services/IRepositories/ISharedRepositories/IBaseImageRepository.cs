using Common.Attributes;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using Common.Interfaces.Entities;

namespace DataAccess.Services.IRepositories.ISharedRepositories;

[Service(nameof(IBaseImageRepository<T>))]
public interface IBaseImageRepository<T> : IBaseModelRepository<T> where T : class, IBaseEntity, ISoftDeletableEntity
{
    public Task<Result<T>> DeleteImageByImageUrl(string imageUrl, bool filterSoftDelete = true, CancellationToken cancellationToken = default);
}
