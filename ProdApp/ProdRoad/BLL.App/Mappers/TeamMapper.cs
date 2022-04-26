using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class TeamMapper : BaseMapper<Team,DAL.App.DTO.Team>
{
    public TeamMapper(IMapper mapper) : base(mapper)
    {
    }
}