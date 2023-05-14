using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class UserTeamMapper : BaseMapper<DAL.App.DTO.UserTeam,Domain.App.UserTeam>
{
    public UserTeamMapper(IMapper mapper) : base(mapper)
    {
        
    }
}