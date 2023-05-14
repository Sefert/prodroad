using AutoMapper;
using Base.Contracts;
using Public.App.Contracts.v1.Services;
using Public.App.DTO.v1.UserTeamDTO;


namespace Public.App.v1.Services;

public class UserTeamService : IUserTeamService
{
    private readonly BLL.App.Contracts.Services.IUserTeamService _service;
    private readonly IMapper<UserTeam, BLL.App.DTO.UserTeam> _mapper;
    
    public UserTeamService(BLL.App.Contracts.Services.IUserTeamService service, IMapper<UserTeam, BLL.App.DTO.UserTeam> mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserTeam>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        return (await _service.GetAllAsync(userId,noTracking)).Select(x => _mapper.Map(x)!);
    }
    
    public async Task<UserTeam?> FirstOrDefaultAsync(Guid id)
    {
        return _mapper.Map(await _service.FirstOrDefaultAsync(id));
    }
    
    public async Task<UserTeam?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return _mapper.Map(await _service.FirstOrDefaultAsync(userId,id,noTracking));
    }
    
    public void ModifyState(UserTeam entity)
    {
        var data = _mapper.Map(entity)!;
        _service.ModifyState(_mapper.Map(entity)!);
    }
    
    public UserTeam Add(UserTeam entity)
    {
        return _mapper.Map(_service.Add(_mapper.Map(entity)!))!;
    }
    
    public async Task<UserTeam> RemoveAsync(Guid id)
    {
        return _mapper.Map(await _service.RemoveAsync(id))!;
    }
    
    public bool Exists(Guid id)
    {
        return _service.Exists(id);
    }
}