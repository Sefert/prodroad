using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.DTO;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class CustomerService :
    BaseEntityService<Customer, DAL.App.DTO.Customer, ICustomerRepository>, 
    ICustomerService
{
    public CustomerService(
        ICustomerRepository repo, 
        IMapper<Customer, DAL.App.DTO.Customer> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    public async Task<Customer?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}