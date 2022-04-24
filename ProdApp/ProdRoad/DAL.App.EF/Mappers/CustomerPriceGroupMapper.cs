using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class CustomerPriceGroupMapper : BaseMapper<DTO.App.CustomerPriceGroup,Domain.App.CustomerPriceGroup>
{
    public CustomerPriceGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}