using Base.Contracts;
using DAL.App.Contracts;
using DAL.App.DTO;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class UserTeamRepository : BaseEntityRepository<DAL.App.DTO.UserTeam, Domain.App.UserTeam, AppDbContext>, IUserTeamRepository
{
    public UserTeamRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.UserTeam, Domain.App.UserTeam> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<DAL.App.DTO.UserTeam>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<DAL.App.DTO.UserTeam?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}