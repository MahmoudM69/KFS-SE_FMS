using Common.Attributes;

namespace DataAccess.Services;

[Service(nameof(IDBInitializer))]
public interface IDBInitializer
{
    void Initialize();
}
