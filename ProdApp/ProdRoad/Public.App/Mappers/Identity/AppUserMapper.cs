using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1.Identity;

namespace Public.App.Mappers.Identity;

//TODO: fix this
public class AppUserMapper : BaseMapper<AppUser,BLL.App.DTO.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}