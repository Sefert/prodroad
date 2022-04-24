using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class TeamMapper : BaseMapper<DTO.App.Team,Domain.App.Team>
{
    public TeamMapper(IMapper mapper) : base(mapper)
    {
    }
}