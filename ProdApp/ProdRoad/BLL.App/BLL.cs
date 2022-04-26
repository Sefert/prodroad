using AutoMapper;
using BLL.App.Contracts;
using BLL.App.Contracts.Services;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using DAL.App.Contracts;

namespace BLL.App;

public class BLL : BaseBll<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UOW;
    private readonly AutoMapper.IMapper _mapper;

    public BLL(IAppUnitOfWork uow, IMapper mapper)
    {
        UOW = uow;
        _mapper = mapper;
    }
    public override async Task<int> SaveChangesAsync()
    {
        return await UOW.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UOW.SaveChanges();
    }

    private IAddressService? _addresses;
    public IAddressService Addresses => _addresses ??= new AddressService(UOW.Addresses,new AddressMapper(_mapper));
}