using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.Mappers;

public class WarehouseMapper : BaseMapper<Warehouse, BLL.App.DTO.Warehouse>
{
    public WarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}