using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class RoadMapMapper : BaseMapper<RoadMap,DAL.App.DTO.RoadMap>
{
    public RoadMapMapper(IMapper mapper) : base(mapper)
    {
    }
}