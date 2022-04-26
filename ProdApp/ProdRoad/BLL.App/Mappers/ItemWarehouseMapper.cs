using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class ItemWarehouseMapper : BaseMapper<ItemWarehouse, DAL.App.DTO.ItemWarehouse>
{
    public ItemWarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}