using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IRoadMapRepository : IEntityRepository<DAL.App.DTO.RoadMap>, 
    IRoadMapRepositoryCustom<DAL.App.DTO.RoadMap>
{

}

public interface IRoadMapRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}