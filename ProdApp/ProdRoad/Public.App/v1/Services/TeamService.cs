using AutoMapper;
using Base.Contracts;
using BLL.App.Contracts.Services;
using Domain.Base;
using Public.App.DTO.v1;

namespace Public.App.v1.Services;

public class TeamService : Public.App.Contracts.v1.Services.ITeamService
{
    private readonly BLL.App.Contracts.Services.ITeamService _service;
    private readonly IMapper<Team, BLL.App.DTO.Team> _mapper;
    
    public TeamService(BLL.App.Contracts.Services.ITeamService service, IMapper<Team, BLL.App.DTO.Team> mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Team>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        return (await _service.GetAllAsync(userId,noTracking)).Select(x => _mapper.Map(x)!);
    }
    
    public async Task<Team?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return _mapper.Map(await _service.FirstOrDefaultAsync(userId,id,noTracking));
    }
    
    public async Task<Team?> PublicTeamAsync(LangStr code, bool noTracking = true)
    {
        return _mapper.Map(await _service.PublicTeamAsync(code,noTracking));
    }
}