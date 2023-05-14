using BLL.App.Contracts;
using Public.App.Contracts.v1;
using Public.App.Contracts.v1.Services;
using Public.App.v1.Mappers;
using Public.App.v1.Services;

namespace Public.App.v1;

using AutoMapper;

public class PublicV1 : IAppPublic
{
    private readonly IAppBLL _appBLL;
    private readonly IMapper _mapper;

    public PublicV1(BLL.App.Contracts.IAppBLL bll, IMapper mapper)
    {
        _appBLL = bll;
        _mapper = mapper;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _appBLL.SaveChangesAsync();
    }
    
    private IUserTeamService? _userTeams;
    public IUserTeamService UserTeams =>
        _userTeams ??= new UserTeamService(_appBLL.UserTeams,new UserTeamMapper(_mapper));
    
    private ITeamService? _teams;
    public ITeamService Teams =>
        _teams ??= new TeamService(_appBLL.Teams,new TeamMapper(_mapper));
    
}