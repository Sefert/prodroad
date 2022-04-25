using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ItemMapper : BaseMapper<DAL.App.DTO.Item,Domain.App.Item>
{
    public ItemMapper(IMapper mapper) : base(mapper)
    {
    }
}