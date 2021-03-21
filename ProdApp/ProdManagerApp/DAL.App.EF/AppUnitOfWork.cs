using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
            ActiveNotifications = new ActiveNotificationRepo(uowDbContext);
            Components = new ComponentRepo(uowDbContext);
            Customers = new CustomerRepo(uowDbContext);
            ItemComponents = new ItemComponentRepo(uowDbContext);
            Items = new ItemRepo(uowDbContext);
            NotificationTypes = new NotificationTypeRepo(uowDbContext);
            Orders = new OrderRepo(uowDbContext);
            OrderDatas = new OrderDataRepo(uowDbContext);
            Prices = new PriceRepo(uowDbContext);
            ProductionMetas = new ProductionMetaRepo(uowDbContext);
            Productions = new ProductionRepo(uowDbContext);
            Supplys = new SupplyRepo(uowDbContext);
            Teams = new TeamRepo(uowDbContext);
            UserNotifications = new UserNotificationRepo(uowDbContext);
            UserTeams = new UserTeamRepo(uowDbContext);
            Warehouses = new WarehouseRepo(uowDbContext);
        }
        public IActiveNotificationRepo ActiveNotifications { get; }
        public IComponentRepo Components { get; }
        public ICustomerRepo Customers { get; }
        public IItemComponentRepo ItemComponents { get;}
        public IItemRepo Items { get;}
        public INotificationTypeRepo NotificationTypes { get; }
        public IOrderDataRepo OrderDatas { get; }
        public IOrderRepo Orders { get; }
        public IPriceRepo Prices { get; }
        public IProductionMetaRepo ProductionMetas { get; }
        public IProductionRepo Productions { get; }
        public ISupplyRepo Supplys { get; }
        public ITeamRepo Teams { get; }
        public IUserNotificationRepo UserNotifications { get; }
        public IUserTeamRepo UserTeams { get; }
        public IWarehouseRepo Warehouses { get; }
    }
}