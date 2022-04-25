using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class CustomerPriceMapper : BaseMapper<DAL.App.DTO.CustomerPrice,Domain.App.CustomerPrice>
{
    public CustomerPriceMapper(IMapper mapper) : base(mapper)
    {
    }
}