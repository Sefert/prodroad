using AutoMapper;
using BLL.App.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App;

public class BLL : BaseBll<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UOW;
    private readonly AutoMapper.IMapper _mapper;

    public BLL(IAppUnitOfWork uow, IMapper mapper)
    {
        UOW = uow;
        _mapper = mapper;
    }
    public override async Task<int> SaveChangesAsync()
    {
        return await UOW.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UOW.SaveChanges();
    }

    private IAddressService? _addresses;
    public IAddressService Addresses => 
        _addresses ??= new AddressService(UOW.Addresses,new AddressMapper(_mapper));
    
    private ICustomerService? _customers;
    public ICustomerService Customers =>
        _customers ??= new CustomerService(UOW.Customers,new CustomerMapper(_mapper));
    
    private ICustomerPriceService? _customerPrices;
    public ICustomerPriceService CustomerPrices => 
        _customerPrices ??= new CustomerPriceService(UOW.CustomerPrices,new CustomerPriceMapper(_mapper));
    
    private ICustomerPriceGroupService? _customerPriceGroups;
    public ICustomerPriceGroupService CustomerPriceGroups => 
        _customerPriceGroups ??= new CustomerPriceGroupService(UOW.CustomerPriceGroups,new CustomerPriceGroupMapper(_mapper));
    
    private IItemService? _items;
    public IItemService Items =>
        _items ??= new ItemService(UOW.Items,new ItemMapper(_mapper));
    
    private IItemProcedureService? _itemProcedures;
    public IItemProcedureService ItemProcedures =>
        _itemProcedures ??= new ItemProcedureService(UOW.ItemProcedures,new ItemProcedureMapper(_mapper));
    
    private IItemWarehouseService? _itemWarehouses;
    public IItemWarehouseService ItemWarehouses =>
        _itemWarehouses ??= new ItemWarehouseService(UOW.ItemWarehouses,new ItemWarehouseMapper(_mapper));
    
    private IPriceService? _prices;
    public IPriceService Prices =>
        _prices ??= new PriceService(UOW.Prices,new PriceMapper(_mapper));
    
    private IPriceGroupService? _priceGroups;
    public IPriceGroupService PriceGroups =>
        _priceGroups ??= new PriceGroupService(UOW.PriceGroups,new PriceGroupMapper(_mapper));
    
    private IProcedureService? _procedures;

    public IProcedureService Procedures =>
        _procedures ??= new ProcedureService(UOW.Procedures,new ProcedureMapper(_mapper));
    
    private IProcessService? _processes;
    public IProcessService Processes =>
        _processes ??= new ProcessService(UOW.Processes,new ProcessMapper(_mapper));
    
    private IRoadMapService? _roadMaps;
    public IRoadMapService RoadMaps =>
        _roadMaps ??= new RoadMapService(UOW.RoadMaps,new RoadMapMapper(_mapper));
    
    private ITeamService? _teams;
    public ITeamService Teams =>
        _teams ??= new TeamService(UOW.Teams,new TeamMapper(_mapper));
    
    private IUserTeamService? _userTeams;
    public IUserTeamService UserTeams =>
        _userTeams ??= new UserTeamService(UOW.UserTeams,new UserTeamMapper(_mapper));
    
    private IWarehouseService? _warehouses;
    public IWarehouseService Warehouses =>
        _warehouses ??= new WarehouseService(UOW.Warehouses,new WarehouseMapper(_mapper));

    
}