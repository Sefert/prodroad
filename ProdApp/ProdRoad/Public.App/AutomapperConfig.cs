using AutoMapper;
using Public.App.DTO.v1;
using Public.App.DTO.v1.Identity;

namespace Public.App;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<AppUser, BLL.App.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<Address, BLL.App.DTO.Address>().ReverseMap();
        CreateMap<Customer, BLL.App.DTO.Customer>().ReverseMap();
        CreateMap<CustomerPrice, BLL.App.DTO.CustomerPrice>().ReverseMap();
        CreateMap<CustomerPriceGroup, BLL.App.DTO.CustomerPriceGroup>().ReverseMap();
        CreateMap<Item, BLL.App.DTO.Item>().ReverseMap();
        CreateMap<ItemProcedure, BLL.App.DTO.ItemProcedure>().ReverseMap();
        CreateMap<ItemWarehouse, BLL.App.DTO.ItemWarehouse>().ReverseMap();
        CreateMap<PriceGroup, BLL.App.DTO.PriceGroup>().ReverseMap();
        CreateMap<Price, BLL.App.DTO.Price>().ReverseMap();
        CreateMap<Procedure, BLL.App.DTO.Procedure>().ReverseMap();
        CreateMap<Process, BLL.App.DTO.Process>().ReverseMap();
        CreateMap<RoadMap, BLL.App.DTO.RoadMap>().ReverseMap();
        CreateMap<Team, BLL.App.DTO.Team>().ReverseMap();
        CreateMap<UserTeam, BLL.App.DTO.UserTeam>().ReverseMap();
        CreateMap<Warehouse, BLL.App.DTO.Warehouse>().ReverseMap();
    }
}