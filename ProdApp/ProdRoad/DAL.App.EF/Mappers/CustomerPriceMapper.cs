using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class CustomerPriceMapper : BaseMapper<DTO.App.CustomerPrice,Domain.App.CustomerPrice>
{
    public CustomerPriceMapper(IMapper mapper) : base(mapper)
    {
    }
}