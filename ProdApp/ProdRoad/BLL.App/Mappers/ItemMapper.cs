using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class ItemMapper : BaseMapper<Item,DAL.App.DTO.Item>
{
    public ItemMapper(IMapper mapper) : base(mapper)
    {
    }
}