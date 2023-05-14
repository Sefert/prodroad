using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;

namespace BLL.App;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
        CreateMap<Address, DAL.App.DTO.Address>().ReverseMap();
        CreateMap<Customer, DAL.App.DTO.Customer>().ReverseMap();
        CreateMap<CustomerPrice, DAL.App.DTO.CustomerPrice>().ReverseMap();
        CreateMap<CustomerPriceGroup, DAL.App.DTO.CustomerPriceGroup>().ReverseMap();
        CreateMap<Item, DAL.App.DTO.Item>().ReverseMap();
        CreateMap<ItemProcedure, DAL.App.DTO.ItemProcedure>().ReverseMap();
        CreateMap<ItemWarehouse, DAL.App.DTO.ItemWarehouse>().ReverseMap();
        CreateMap<PriceGroup, DAL.App.DTO.PriceGroup>().ReverseMap();
        CreateMap<Price, DAL.App.DTO.Price>().ReverseMap();
        CreateMap<Procedure, DAL.App.DTO.Procedure>().ReverseMap();
        CreateMap<Process, DAL.App.DTO.Process>().ReverseMap();
        CreateMap<RoadMap, DAL.App.DTO.RoadMap>().ReverseMap();
        CreateMap<Team, DAL.App.DTO.Team>().ReverseMap();
        CreateMap<UserTeam, DAL.App.DTO.UserTeam>().
            ForMember(uT => uT.Team,
                options => 
                    options.MapFrom(uT => uT.Team)
            ).ReverseMap();
        CreateMap<Warehouse, DAL.App.DTO.Warehouse>().ReverseMap();
    }
}