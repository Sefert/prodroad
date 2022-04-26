using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class ProcedureMapper : BaseMapper<Procedure,DAL.App.DTO.Procedure>
{
    public ProcedureMapper(IMapper mapper) : base(mapper)
    {
    }
}