namespace Base.Contracts.Domain;

public interface IPlanMeta
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public DateTime RealStartAt { get; set; }
    public DateTime RealEndAt { get; set; }
}