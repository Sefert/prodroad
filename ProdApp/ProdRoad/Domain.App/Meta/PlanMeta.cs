namespace Domain.App.Meta;

public abstract class PlanMeta : UpdateMeta
{
    public DateTime StartAt { get; set; } = DateTime.UtcNow;
    public DateTime EndAt { get; set; } = DateTime.UtcNow;
    public DateTime RealStartAt { get; set; } = DateTime.UtcNow;
    public DateTime RealEndAt { get; set; } = DateTime.UtcNow;
}