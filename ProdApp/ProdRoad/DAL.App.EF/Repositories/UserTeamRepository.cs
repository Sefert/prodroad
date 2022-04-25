using Base.Contracts;
using DAL.App.Contracts;
using DAL.Base.EF;

namespace DAL.App.EF.Repositories;

public class UserTeamRepository : BaseEntityRepository<DAL.App.DTO.UserTeam, Domain.App.UserTeam, AppDbContext>, IUserTeamRepository
{
    public UserTeamRepository(AppDbContext dbContext, IMapper<DAL.App.DTO.UserTeam, Domain.App.UserTeam> mapper) : base(dbContext, mapper)
    {
    }
}