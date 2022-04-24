using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class UserTeamRepository : BaseEntityRepository<DTO.App.UserTeam, Domain.App.UserTeam, AppDbContext>, IUserTeamRepository
{
    public UserTeamRepository(AppDbContext dbContext, IMapper<DTO.App.UserTeam, Domain.App.UserTeam> mapper) : base(dbContext, mapper)
    {
    }
}