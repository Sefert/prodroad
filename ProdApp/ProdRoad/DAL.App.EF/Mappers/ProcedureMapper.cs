using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ProcedureMapper : BaseMapper<DTO.App.Procedure,Domain.App.Procedure>
{
    public ProcedureMapper(IMapper mapper) : base(mapper)
    {
    }
}