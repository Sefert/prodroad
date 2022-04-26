
using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers.Identity;

public class AppUserMapper : BaseMapper<Address,DAL.App.DTO.Address>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}