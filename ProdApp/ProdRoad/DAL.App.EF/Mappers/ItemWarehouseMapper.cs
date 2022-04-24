using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ItemWarehouseMapper : BaseMapper<DTO.App.ItemWarehouse,Domain.App.ItemWarehouse>
{
    public ItemWarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}