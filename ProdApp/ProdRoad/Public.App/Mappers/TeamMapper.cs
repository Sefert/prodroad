using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.Mappers;

public class TeamMapper : BaseMapper<Team, BLL.App.DTO.Team>
{
    public TeamMapper(IMapper mapper) : base(mapper)
    {
    }
}