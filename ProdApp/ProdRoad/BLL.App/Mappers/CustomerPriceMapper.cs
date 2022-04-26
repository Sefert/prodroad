using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;


public class CustomerPriceMapper : BaseMapper<CustomerPrice,DAL.App.DTO.CustomerPrice>
{
    public CustomerPriceMapper(IMapper mapper) : base(mapper)
    {
    }
}