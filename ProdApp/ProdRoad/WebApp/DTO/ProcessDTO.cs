using Domain.App;
using Domain.App.Meta;

namespace WebApp.DTO;

public class ProcessDTO : PlanMeta
{
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    
    public Guid? RoadMapId { get; set; }
    public RoadMap? RoadMap { get; set; }

    public Guid ProcedureId { get; set; }
    public Procedure? Procedure { get; set; }

    public decimal CreatedAmount { get; set; }

    public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
    
}