using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.v1.Mappers;

public class ItemMapper : BaseMapper<Item,BLL.App.DTO.Item>
{
    public ItemMapper(IMapper mapper) : base(mapper)
    {
    }
}