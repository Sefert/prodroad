using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class CustomerPriceGroupMapper : BaseMapper<CustomerPriceGroup,DAL.App.DTO.CustomerPriceGroup>
{
    public CustomerPriceGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}