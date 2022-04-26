using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class ProcessMapper : BaseMapper<Process,DAL.App.DTO.Process>
{
    public ProcessMapper(IMapper mapper) : base(mapper)
    {
    }
}