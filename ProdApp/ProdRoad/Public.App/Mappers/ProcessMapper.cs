using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.Mappers;

public class ProcessMapper : BaseMapper<Process, BLL.App.DTO.Process>
{
    public ProcessMapper(IMapper mapper) : base(mapper)
    {
    }
}