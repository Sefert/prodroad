using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ItemMapper : BaseMapper<DTO.App.Item,Domain.App.Item>
{
    public ItemMapper(IMapper mapper) : base(mapper)
    {
    }
}