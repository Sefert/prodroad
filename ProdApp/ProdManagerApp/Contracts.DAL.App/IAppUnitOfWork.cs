using Contracts.DAL.App.Repositories;
using Contracts.DAL.BAse;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IActiveNotificationRepo ActiveNotifications { get; }
        IComponentRepo Components { get; }
        ICustomerRepo Customers { get; }
        IItemRepo Items { get;}
        INotificationTypeRepo NotificationTypes { get; }
        IOrderDataRepo OrderDatas { get; }
        IOrderRepo Orders { get; }
        IPriceRepo Prices { get; }
        IProductionMetaRepo ProductionMetas { get; }
        IProductionRepo Productions { get; }
        ISupplyRepo Supplys { get; }
        ITeamRepo Teams { get; }
        IUserNotificationRepo UserNotifications { get; }
        IUserTeamRepo UserTeams { get; }
        IWarehouseRepo Warehouses { get; }
        IUserUnitRepo UserUnits { get; }
    }
}