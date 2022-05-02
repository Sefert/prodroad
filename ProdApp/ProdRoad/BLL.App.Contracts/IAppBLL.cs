using Base.Contracts.BLL;
using BLL.App.Contracts.Services;

namespace BLL.App.Contracts;

public interface IAppBLL : IBLL
{
    IAddressService Addresses { get; }
    ICustomerPriceGroupService CustomerPriceGroups { get; }
    ICustomerPriceService CustomerPrices { get; }
    ICustomerService Customers{ get; }
    IItemService Items { get; }
    IItemProcedureService ItemProcedures { get; }
    IItemWarehouseService ItemWarehouses { get; }
    IPriceGroupService PriceGroups { get; }
    IPriceService Prices { get; }
    IProcedureService Procedures { get; }
    IProcessService Processes { get; }
    IRoadMapService RoadMaps { get; }
    ITeamService Teams { get; }
    IUserTeamService UserTeams { get; }
    IWarehouseService Warehouses { get; }
    
}