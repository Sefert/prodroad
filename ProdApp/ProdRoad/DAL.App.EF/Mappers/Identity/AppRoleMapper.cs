using AutoMapper;
using DAL.App.DTO.Identity;
using DAL.Base;

namespace DAL.App.EF.Mappers.Identity;

public class AppRoleMapper : BaseMapper<AppRole,Domain.App.Identity.AppRole>
{
    public AppRoleMapper(IMapper mapper) : base(mapper)
    {
    }
}