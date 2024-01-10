namespace Common.Interfaces.Entities;

public interface ISoftDeletableEntity
{
    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
