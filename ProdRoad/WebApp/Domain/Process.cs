namespace WebApp.Domain;

public class Process : BaseEntity
{
    public Guid RoadMapId { get; set; }
    public RoadMap RoadMap { get; set; } = default!;
    
    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
    
    public DateTime StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public DateTime? RealStartAt { get; set; }
    public DateTime? RealEndAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<ItemProcess>? ItemProcesses { get; set; }
}