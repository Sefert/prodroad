using AutoMapper;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace DAL.App.EF.Mappers.Identity;

public class AppUserMapper : BaseMapper<AppUser,Domain.App.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}