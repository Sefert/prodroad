using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class CustomerPriceGroupMapper : BaseMapper<DAL.App.DTO.CustomerPriceGroup,Domain.App.CustomerPriceGroup>
{
    public CustomerPriceGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}