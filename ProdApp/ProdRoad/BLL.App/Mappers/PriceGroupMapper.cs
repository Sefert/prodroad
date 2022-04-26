using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class PriceGroupMapper : BaseMapper<PriceGroup,DAL.App.DTO.PriceGroup>
{
    public PriceGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}