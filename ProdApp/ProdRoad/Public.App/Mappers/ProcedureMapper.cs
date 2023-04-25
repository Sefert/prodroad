using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.Mappers;

public class ProcedureMapper : BaseMapper<Procedure, BLL.App.DTO.Procedure>
{
    public ProcedureMapper(IMapper mapper) : base(mapper)
    {
    }
}