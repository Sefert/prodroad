using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.v1.Mappers;

public class TeamMapper : BaseMapper<Team, BLL.App.DTO.Team>
{
    public TeamMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public Public.App.DTO.v1.UserTeamDTO.Team MapTeamWithoutUserTeam(BLL.App.DTO.Team entity)
    {
        //var res = Mapper.Map<Public.DTO.v1.TrainingPlan>(entity);

        var res = new Public.App.DTO.v1.UserTeamDTO.Team()
        {
            Id = entity.Id,
            Name = entity.Name,
            Code = entity.Code,
            IsPublic = entity.IsPublic,
        };
        
        return res;
    }
}