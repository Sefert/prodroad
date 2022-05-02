using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IProcedureRepository : IEntityRepository<DAL.App.DTO.Procedure>,
    IProcedureRepositoryCustom<DAL.App.DTO.Procedure>
{
    
}

public interface IProcedureRepositoryCustom<TEntity>
    where TEntity: class
{
    //custom methods here (search, so on)
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true);
}