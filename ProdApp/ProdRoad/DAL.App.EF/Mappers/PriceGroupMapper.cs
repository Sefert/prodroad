using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class PriceGroupMapper : BaseMapper<DAL.App.DTO.PriceGroup,Domain.App.PriceGroup>
{
    public PriceGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}