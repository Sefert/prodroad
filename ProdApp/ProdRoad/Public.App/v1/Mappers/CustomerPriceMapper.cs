using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;
namespace Public.App.v1.Mappers;


public class CustomerPriceMapper : BaseMapper<CustomerPrice,BLL.App.DTO.CustomerPrice>
{
    public CustomerPriceMapper(IMapper mapper) : base(mapper)
    {
    }
}