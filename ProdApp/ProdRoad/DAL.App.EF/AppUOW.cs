#nullable enable
using DAL.App.Contracts;
using DAL.App.EF.Mappers;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF;

public class AppUOW : BaseUOW<AppDbContext>,  IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;
    public AppUOW(AppDbContext uowDbContext, AutoMapper.IMapper mapper) : base(uowDbContext)
    {
        _mapper = mapper;
    }

    //one possible way (lazy initialization), better way is to use factory pattern
    //private IAddressRepository? _addresses;
    //public virtual IAddressRepository Addresses => 
    //    _addresses ??= new AddressRepository(UOWDbContext);

    public virtual IAddressRepository Addresses =>
        GetRepository(() =>new AddressRepository(UOWDbContext, new AddressMapper(_mapper)));
    /*public virtual IActiveNotificationRepository ActiveNotifications =>
        GetRepository(() =>new ActiveNotificationRepository(UOWDbContext));*/
    public ICustomerRepository Customers =>
        GetRepository(() =>new CustomerRepository(UOWDbContext, new CustomerMapper(_mapper)));
    public ICustomerPriceRepository CustomerPrices =>
        GetRepository(() =>new CustomerPriceRepository(UOWDbContext, new CustomerPriceMapper(_mapper)));
    public ICustomerPriceGroupRepository CustomerPriceGroups =>
        GetRepository(() =>new CustomerPriceGroupRepository(UOWDbContext, new CustomerPriceGroupMapper(_mapper)));
    public IItemRepository Items =>
        GetRepository(() =>new ItemRepository(UOWDbContext, new ItemMapper(_mapper)));
    public IItemProcedureRepository ItemProcedures =>
        GetRepository(() =>new ItemProcedureRepository(UOWDbContext, new ItemProcedureMapper(_mapper)));
    public IItemWarehouseRepository ItemWarehouses =>
        GetRepository(() =>new ItemWarehouseRepository(UOWDbContext, new ItemWarehouseMapper(_mapper)));
    public IPriceRepository Prices =>
        GetRepository(() =>new PriceRepository(UOWDbContext, new PriceMapper(_mapper)));
    public IPriceGroupRepository PriceGroups =>
        GetRepository(() =>new PriceGroupRepository(UOWDbContext, new PriceGroupMapper(_mapper)));
    public IProcedureRepository Procedures =>
        GetRepository(() =>new ProcedureRepository(UOWDbContext, new ProcedureMapper(_mapper)));
    public IProcessRepository Processes =>
        GetRepository(() =>new ProcessRepository(UOWDbContext, new ProcessMapper(_mapper)));
    public IRoadMapRepository RoadMaps =>
        GetRepository(() =>new RoadMapRepository(UOWDbContext, new RoadMapMapper(_mapper)));
    public ITeamRepository Teams =>
        GetRepository(() =>new TeamRepository(UOWDbContext, new TeamMapper(_mapper)));
    /*public IUserNotificationRepository UserNotifications =>
        GetRepository(() =>new UserNotificationRepository(UOWDbContext));*/
    public IUserTeamRepository UserTeams =>
        GetRepository(() =>new UserTeamRepository(UOWDbContext, new UserTeamMapper(_mapper)));
    public IWarehouseRepository Warehouses =>
        GetRepository(() =>new WarehouseRepository(UOWDbContext, new WarehouseMapper(_mapper)));
}