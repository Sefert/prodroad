using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ItemWarehouseMapper : BaseMapper<DAL.App.DTO.ItemWarehouse,Domain.App.ItemWarehouse>
{
    public ItemWarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}