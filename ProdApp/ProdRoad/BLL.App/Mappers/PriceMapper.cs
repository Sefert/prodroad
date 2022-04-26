using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PriceMapper : BaseMapper<Price,DAL.App.DTO.Price>
{
    public PriceMapper(IMapper mapper) : base(mapper)
    {
    }
}