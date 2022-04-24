using AutoMapper;

namespace DAL.App.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<DTO.App.Address, Domain.App.Address>().ReverseMap();
        CreateMap<DTO.App.Customer, Domain.App.Customer>().ReverseMap();
        CreateMap<DTO.App.CustomerPriceGroup, Domain.App.CustomerPriceGroup>().ReverseMap();
        CreateMap<DTO.App.CustomerPrice, Domain.App.CustomerPrice>().ReverseMap();
        CreateMap<DTO.App.Item, Domain.App.Item>().ReverseMap();
        CreateMap<DTO.App.ItemProcedure, Domain.App.ItemProcedure>().ReverseMap();
        CreateMap<DTO.App.ItemWarehouse, Domain.App.ItemWarehouse>().ReverseMap();
        CreateMap<DTO.App.PriceGroup, Domain.App.PriceGroup>().ReverseMap();
        CreateMap<DTO.App.Price, Domain.App.Price>().ReverseMap();
        CreateMap<DTO.App.Procedure, Domain.App.Procedure>().ReverseMap();
        CreateMap<DTO.App.Process, Domain.App.Process>().ReverseMap();
        CreateMap<DTO.App.RoadMap, Domain.App.RoadMap>().ReverseMap();
        CreateMap<DTO.App.Team, Domain.App.Team>().ReverseMap();
        CreateMap<DTO.App.UserTeam, Domain.App.UserTeam>().ReverseMap();
        CreateMap<DTO.App.Warehouse, Domain.App.Warehouse>().ReverseMap();
    }
}