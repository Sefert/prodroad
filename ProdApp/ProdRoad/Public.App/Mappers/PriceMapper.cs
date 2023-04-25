using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.Mappers;

public class PriceMapper : BaseMapper<Price, BLL.App.DTO.Price>
{
    public PriceMapper(IMapper mapper) : base(mapper)
    {
    }
}