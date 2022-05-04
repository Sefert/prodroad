
using AutoMapper;
using BLL.App.DTO.Identity;
using DAL.Base;

namespace BLL.App.Mappers.Identity;

//TODO: fix this
public class AppUserMapper : BaseMapper<AppUser,DAL.App.DTO.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}