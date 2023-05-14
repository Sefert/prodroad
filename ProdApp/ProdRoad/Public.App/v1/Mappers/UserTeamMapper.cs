using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1.UserTeamDTO;

namespace Public.App.v1.Mappers;

public class UserTeamMapper : BaseMapper<UserTeam, BLL.App.DTO.UserTeam>
{
    public UserTeamMapper(IMapper mapper) : base(mapper)
    {
    }

}