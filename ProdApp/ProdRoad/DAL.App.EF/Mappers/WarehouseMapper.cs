using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class WarehouseMapper : BaseMapper<DTO.App.Warehouse,Domain.App.Warehouse>
{
    public WarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}