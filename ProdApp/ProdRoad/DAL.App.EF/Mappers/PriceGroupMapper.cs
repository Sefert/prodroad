using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class PriceGroupMapper : BaseMapper<DTO.App.PriceGroup,Domain.App.PriceGroup>
{
    public PriceGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}