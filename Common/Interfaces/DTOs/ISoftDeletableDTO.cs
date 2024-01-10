namespace Common.Interfaces.DTOs;

public interface ISoftDeletableDTO
{
    public int SoftDelete { get; set; }
    public bool IsRecoverable { get; set; }
}
