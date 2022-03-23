#nullable enable
using DAL.App.Contracts;
using DAL.App.EF.Repositories;

namespace DAL.App.EF;

public class AppUOW : IAppUnitOfWork
{
    protected readonly AppDbContext UOWDbContext;
    
    public AppUOW(AppDbContext uowDbContext)
    {
        UOWDbContext = uowDbContext;
    }
    public virtual async Task<int> SaveChangesAsync()
    {
        return await UOWDbContext.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
        return UOWDbContext.SaveChanges();
    }

    //one possible way (lazy initialization)
    private IAddressRepository? _addresses;
    public virtual IAddressRepository Addresses => 
        _addresses ??= new AddressRepository(UOWDbContext);
}