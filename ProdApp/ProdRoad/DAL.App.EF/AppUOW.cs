#nullable enable
using DAL.App.Contracts;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF;

public class AppUOW : BaseUOW<AppDbContext>,  IAppUnitOfWork
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }

    //one possible way (lazy initialization), better way is to use factory pattern
    private IAddressRepository? _addresses;
    public virtual IAddressRepository Addresses => 
        _addresses ??= new AddressRepository(UOWDbContext);
}