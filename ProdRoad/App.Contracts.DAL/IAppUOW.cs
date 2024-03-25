using App.Contracts.DAL.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUOW
{
    public IAddressRepository Addresses { get; }
    public ICustomerRepository Customers { get; }
    public ICustomerPriceRepository CustomerPrices { get; }
    public IPriceRepository Prices { get; }
    public IItemRepository Items { get; }
    public IItemProcessRepository ItemProcess { get; }
    public IProcessRepository Processes { get; }
    public IOrderRepository Orders { get; }
    public IOrderRowRepository OrderRows { get; }
    public IRoadMapRepository RoadMaps { get; }
    public ITeamRepository Teams { get; }
    public IUserTeamRepository UserTeams { get; }
    public IEntityRepository<AppUser> Users { get; }
}