using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class WarehouseMapper : BaseMapper<Warehouse,DAL.App.DTO.Warehouse>
{
    public WarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}