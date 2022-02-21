using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class Process : PlanMeta
{
    public Guid TeamId { get; set; }
    public Team Team { get; set; } = default!;
    
    public Guid? RoadMapId { get; set; }
    public RoadMap? RoadMap { get; set; }

    public Guid ProcedureId { get; set; }
    public Procedure Procedure { get; set; } = default!;

    public decimal CreatedAmount { get; set; } = default!;

    public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
    
}