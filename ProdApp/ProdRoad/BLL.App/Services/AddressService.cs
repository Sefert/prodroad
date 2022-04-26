using Base.Contracts;
using BLL.App.Contracts.Services;
using BLL.Base;
using BLL.App.DTO;
using DAL.App.Contracts;

namespace BLL.App.Services;

public class AddressService : BaseEntityService<Address, DAL.App.DTO.Address, IAddressRepository>, IAddressService
{
    public AddressService(IAddressRepository repo, IMapper<Address, DAL.App.DTO.Address> mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<Address>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        return (await Repo.GetAllAsync(userId,noTracking)).Select(x => Mapper.Map(x)!);
    }

    public async Task<Address?> FirstOrDefaultAsync(Guid userId, Guid id, bool noTracking = true)
    {
        return Mapper.Map(await Repo.FirstOrDefaultAsync(userId,noTracking));
    }
}