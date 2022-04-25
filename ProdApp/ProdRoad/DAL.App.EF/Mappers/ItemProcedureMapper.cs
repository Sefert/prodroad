using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ItemProcedureMapper : BaseMapper<DAL.App.DTO.ItemProcedure,Domain.App.ItemProcedure>
{
    public ItemProcedureMapper(IMapper mapper) : base(mapper)
    {
    }
}