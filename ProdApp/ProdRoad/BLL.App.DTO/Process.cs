using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO;

public class Process : BaseEntity
{
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    
    public Guid? RoadMapId { get; set; }
    public RoadMap? RoadMap { get; set; }

    public Guid ProcedureId { get; set; }
    public Procedure? Procedure { get; set; }

    public decimal CreatedAmount { get; set; }
}