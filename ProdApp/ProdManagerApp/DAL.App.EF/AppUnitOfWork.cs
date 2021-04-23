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
        }
        public IActiveNotificationRepo ActiveNotifications => GetRepository(() => new ActiveNotificationRepo(UowDbContext));
        public IComponentRepo Components => GetRepository(() => new ComponentRepo(UowDbContext));
        public ICustomerRepo Customers => GetRepository(() => new CustomerRepo(UowDbContext));
        public IItemComponentRepo ItemComponents => GetRepository(() => new ItemComponentRepo(UowDbContext));
        public IItemRepo Items => GetRepository(() => new ItemRepo(UowDbContext));
        public INotificationTypeRepo NotificationTypes => GetRepository(() => new NotificationTypeRepo(UowDbContext));
        public IOrderDataRepo OrderDatas => GetRepository(() => new OrderDataRepo(UowDbContext));
        public IOrderRepo Orders => GetRepository(() => new OrderRepo(UowDbContext));
        public IPriceRepo Prices => GetRepository(() => new PriceRepo(UowDbContext));
        public IProductionMetaRepo ProductionMetas => GetRepository(() => new ProductionMetaRepo(UowDbContext));
        public IProductionRepo Productions => GetRepository(() => new ProductionRepo(UowDbContext));
        public ISupplyRepo Supplys => GetRepository(() => new SupplyRepo(UowDbContext));
        public ITeamRepo Teams => GetRepository(() => new TeamRepo(UowDbContext));
        public IUserNotificationRepo UserNotifications => GetRepository(() => new UserNotificationRepo(UowDbContext));
        public IUserTeamRepo UserTeams => GetRepository(() => new UserTeamRepo(UowDbContext));
        public IWarehouseRepo Warehouses => GetRepository(() => new WarehouseRepo(UowDbContext));
        public IUserUnitRepo UserUnits => GetRepository(() => new UserUnitRepo(UowDbContext));
    }
}