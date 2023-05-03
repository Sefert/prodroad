using Base.Contracts.DAL;
using Domain.Base;

namespace DAL.App.Contracts;

public interface ITeamRepository : IEntityRepository<DAL.App.DTO.Team>, 
    ITeamRepositoryCustom<DAL.App.DTO.Team>
{
}

public interface ITeamRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
    Task<TEntity?> PublicTeamAsync(LangStr code, bool noTracking = true);
}