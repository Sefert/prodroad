using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.v1.Mappers;

public class ItemProcedureMapper : BaseMapper<ItemProcedure,BLL.App.DTO.ItemProcedure>
{
    public ItemProcedureMapper(IMapper mapper) : base(mapper)
    {
    }
}