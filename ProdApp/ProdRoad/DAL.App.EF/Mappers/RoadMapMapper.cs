using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class RoadMapMapper : BaseMapper<DTO.App.RoadMap,Domain.App.RoadMap>
{
    public RoadMapMapper(IMapper mapper) : base(mapper)
    {
    }
}