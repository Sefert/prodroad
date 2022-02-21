namespace Domain.App;

public abstract class PlanMeta : UpdateMeta
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public DateTime? RealStartAt { get; set; }
    public DateTime? RealEndAt { get; set; }
}