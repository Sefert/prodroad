
using App.Contracts.DAL;
using App.Contracts.DAL.Repositories;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using Base.Contracts.DAL;
using Base.DAL.EF;

public class AppUOW : BaseUnitOfWork<AppDbContext>, IAppUOW
{
    public AppUOW(AppDbContext dbContext) : base(dbContext)
    {
    }

    private IAddressRepository? _addresses;
    public IAddressRepository Addresses => _addresses ?? new AddressRepository(UowDbContext);
    
    private ICustomerRepository? _customers;
    public ICustomerRepository Customers => _customers ?? new CustomerRepository(UowDbContext);
    
    private ICustomerPriceRepository? _customerPrices;
    public ICustomerPriceRepository CustomerPrices => _customerPrices ?? new CustomerPriceRepository(UowDbContext);
    
    private IPriceRepository? _prices;
    public IPriceRepository Prices => _prices ?? new PriceRepository(UowDbContext);
    
    private IItemRepository? _items;
    public IItemRepository Items => _items ?? new ItemRepository(UowDbContext);
    
    private IItemProcessRepository? _itemprocesses;
    public IItemProcessRepository ItemProcess => _itemprocesses ?? new ItemProcessRepository(UowDbContext);
    
    private IProcessRepository? _processes;
    public IProcessRepository Processes => _processes ?? new ProcessRepository(UowDbContext);
    
    private IOrderRepository? _orders;
    public IOrderRepository Orders => _orders ?? new OrderRepository(UowDbContext);
    
    private IOrderRowRepository? _orderRows;
    public IOrderRowRepository OrderRows => _orderRows ?? new OrderRowRepository(UowDbContext);
    
    private RoadMapRepository? _roadmaps;
    public IRoadMapRepository RoadMaps => _roadmaps ?? new RoadMapRepository(UowDbContext);
    
    private ITeamRepository? _teams;
    public ITeamRepository Teams => _teams ?? new TeamRepository(UowDbContext);
        
    private IUserTeamRepository? _userTeams;
    public IUserTeamRepository UserTeams => _userTeams ?? new UserTeamRepository(UowDbContext);

    private IEntityRepository<AppUser>? _users;
    public IEntityRepository<AppUser> Users => _users ??
                                               new BaseEntityRepository<AppDbContext, AppUser, AppUser>(UowDbContext,
                                                   new DalMapper<AppUser, AppUser>());
}