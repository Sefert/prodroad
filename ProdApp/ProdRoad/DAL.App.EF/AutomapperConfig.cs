using AutoMapper;
using DAL.App.DTO.Identity;

namespace DAL.App.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
        CreateMap<DAL.App.DTO.Address, Domain.App.Address>().ReverseMap();
        CreateMap<DAL.App.DTO.Customer, Domain.App.Customer>().ReverseMap();
        CreateMap<DAL.App.DTO.CustomerPriceGroup, Domain.App.CustomerPriceGroup>().ReverseMap();
        CreateMap<DAL.App.DTO.CustomerPrice, Domain.App.CustomerPrice>().ReverseMap();
        CreateMap<DAL.App.DTO.Item, Domain.App.Item>().ReverseMap();
        CreateMap<DAL.App.DTO.ItemProcedure, Domain.App.ItemProcedure>().ReverseMap();
        CreateMap<DAL.App.DTO.ItemWarehouse, Domain.App.ItemWarehouse>().ReverseMap();
        CreateMap<DAL.App.DTO.PriceGroup, Domain.App.PriceGroup>().ReverseMap();
        CreateMap<DAL.App.DTO.Price, Domain.App.Price>().ReverseMap();
        CreateMap<DAL.App.DTO.Procedure, Domain.App.Procedure>().ReverseMap();
        CreateMap<DAL.App.DTO.Process, Domain.App.Process>().ReverseMap();
        CreateMap<DAL.App.DTO.RoadMap, Domain.App.RoadMap>().ReverseMap();
        CreateMap<DAL.App.DTO.Team, Domain.App.Team>().ReverseMap();
        CreateMap<DAL.App.DTO.UserTeam, Domain.App.UserTeam>().ReverseMap();
        CreateMap<DAL.App.DTO.Warehouse, Domain.App.Warehouse>().ReverseMap();
    }
}