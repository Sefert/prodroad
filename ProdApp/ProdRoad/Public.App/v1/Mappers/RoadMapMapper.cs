using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.v1.Mappers;

public class RoadMapMapper : BaseMapper<RoadMap, BLL.App.DTO.RoadMap>
{
    public RoadMapMapper(IMapper mapper) : base(mapper)
    {
    }
}