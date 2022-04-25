using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ProcedureMapper : BaseMapper<DAL.App.DTO.Procedure,Domain.App.Procedure>
{
    public ProcedureMapper(IMapper mapper) : base(mapper)
    {
    }
}