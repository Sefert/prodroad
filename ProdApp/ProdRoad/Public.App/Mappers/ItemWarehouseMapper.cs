using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.Mappers;

public class ItemWarehouseMapper : BaseMapper<ItemWarehouse, BLL.App.DTO.ItemWarehouse>
{
    public ItemWarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}