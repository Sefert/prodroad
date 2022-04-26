using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class UserTeamMapper : BaseMapper<UserTeam,DAL.App.DTO.UserTeam>
{
    public UserTeamMapper(IMapper mapper) : base(mapper)
    {
    }
}