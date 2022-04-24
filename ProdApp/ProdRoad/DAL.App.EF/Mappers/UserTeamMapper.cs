using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class UserTeamMapper : BaseMapper<DTO.App.UserTeam,Domain.App.UserTeam>
{
    public UserTeamMapper(IMapper mapper) : base(mapper)
    {
    }
}