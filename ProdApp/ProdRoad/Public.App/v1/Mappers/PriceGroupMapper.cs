using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.v1.Mappers;

public class PriceGroupMapper : BaseMapper<PriceGroup, BLL.App.DTO.PriceGroup>
{
    public PriceGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}