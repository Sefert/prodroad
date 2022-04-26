using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class ItemProcedureMapper : BaseMapper<ItemProcedure,DAL.App.DTO.ItemProcedure>
{
    public ItemProcedureMapper(IMapper mapper) : base(mapper)
    {
    }
}