using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class ProcessMapper : BaseMapper<DTO.App.Process,Domain.App.Process>
{
    public ProcessMapper(IMapper mapper) : base(mapper)
    {
    }
}