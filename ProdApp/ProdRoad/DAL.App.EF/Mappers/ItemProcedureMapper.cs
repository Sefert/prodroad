using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ItemProcedureMapper : BaseMapper<DTO.App.ItemProcedure,Domain.App.ItemProcedure>
{
    public ItemProcedureMapper(IMapper mapper) : base(mapper)
    {
    }
}