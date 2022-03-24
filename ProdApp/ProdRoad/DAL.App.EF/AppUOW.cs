#nullable enable
using DAL.App.Contracts;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF;

public class AppUOW : BaseUOW<AppDbContext>,  IAppUnitOfWork
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }

    //one possible way (lazy initialization), better way is to use factory pattern
    //private IAddressRepository? _addresses;
    //public virtual IAddressRepository Addresses => 
    //    _addresses ??= new AddressRepository(UOWDbContext);

    public virtual IAddressRepository Addresses =>
        GetRepository(() =>new AddressRepository(UOWDbContext));
    public virtual IActiveNotificationRepository ActiveNotifications =>
        GetRepository(() =>new ActiveNotificationRepository(UOWDbContext));
    public ICustomerRepository Customers =>
        GetRepository(() =>new CustomerRepository(UOWDbContext));
    public ICustomerPriceRepository CustomerPrices =>
        GetRepository(() =>new CustomerPriceRepository(UOWDbContext));
    public ICustomerPriceGroupRepository CustomerPriceGroups =>
        GetRepository(() =>new CustomerPriceGroupRepository(UOWDbContext));
    public IItemRepository Items =>
        GetRepository(() =>new ItemRepository(UOWDbContext));
    public IItemProcedureRepository ItemProcedures =>
        GetRepository(() =>new ItemProcedureRepository(UOWDbContext));
    public IItemWarehouseRepository ItemWarehouses =>
        GetRepository(() =>new ItemWarehouseRepository(UOWDbContext));
    public IPriceRepository Prices =>
        GetRepository(() =>new PriceRepository(UOWDbContext));
    public IPriceGroupRepository PriceGroups =>
        GetRepository(() =>new PriceGroupRepository(UOWDbContext));
    public IProcedureRepository Procedures =>
        GetRepository(() =>new ProcedureRepository(UOWDbContext));
    public IProcessRepository Processes =>
        GetRepository(() =>new ProcessRepository(UOWDbContext));
    public IRoadMapRepository RoadMaps =>
        GetRepository(() =>new RoadMapRepository(UOWDbContext));
    public ITeamRepository Teams =>
        GetRepository(() =>new TeamRepository(UOWDbContext));
    public IUserNotificationRepository UserNotifications =>
        GetRepository(() =>new UserNotificationRepository(UOWDbContext));
    public IUserTeamRepository UserTeams =>
        GetRepository(() =>new UserTeamRepository(UOWDbContext));
    public IWarehouseRepository Warehouses =>
        GetRepository(() =>new WarehouseRepository(UOWDbContext));
}