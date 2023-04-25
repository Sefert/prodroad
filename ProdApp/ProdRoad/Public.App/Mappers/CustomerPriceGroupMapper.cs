using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.Mappers;

public class CustomerPriceGroupMapper : BaseMapper<CustomerPriceGroup,BLL.App.DTO.CustomerPriceGroup>
{
    public CustomerPriceGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}