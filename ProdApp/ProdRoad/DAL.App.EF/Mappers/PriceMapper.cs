using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class PriceMapper : BaseMapper<DTO.App.Price,Domain.App.Price>
{
    public PriceMapper(IMapper mapper) : base(mapper)
    {
    }
}