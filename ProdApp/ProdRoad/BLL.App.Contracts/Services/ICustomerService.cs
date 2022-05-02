using Base.Contracts.BLL;
using DAL.App.Contracts;


namespace BLL.App.Contracts.Services;

public interface ICustomerService : IEntityService<BLL.App.DTO.Customer>, 
    ICustomerRepositoryCustom<BLL.App.DTO.Customer>
{
    
}