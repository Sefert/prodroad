#nullable enable
using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IAppUnitOfWork : IUnitOfWork
{
    IAddressRepository Addresses { get; }
    IActiveNotificationRepository ActiveNotifications { get; }
    ICustomerRepository Customers { get; }
    ICustomerPriceRepository CustomerPrices { get; }
    ICustomerPriceGroupRepository CustomerPriceGroups { get; }
    IItemRepository Items { get; }
    IItemProcedureRepository ItemProcedures { get; }
    IItemWarehouseRepository ItemWarehouses { get; }
    IPriceRepository Prices { get; }
    IPriceGroupRepository PriceGroups { get; }
    IProcedureRepository Procedures { get; }
    IProcessRepository Processes { get; }
    IRoadMapRepository RoadMaps { get; }
    ITeamRepository Teams { get; }
    IUserNotificationRepository UserNotifications { get; }
    IUserTeamRepository UserTeams { get; }
    IWarehouseRepository Warehouses { get; }
}