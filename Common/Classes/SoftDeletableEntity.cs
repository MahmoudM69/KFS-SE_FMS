using Common.Interfaces.Entities;

namespace Common.Classes;

public class SoftDeletableEntity : BaseEntity, ISoftDeletableEntity
{
    public int SoftDelete { get; set; } = 0;
    public bool IsRecoverable { get; set; } = true;
}
