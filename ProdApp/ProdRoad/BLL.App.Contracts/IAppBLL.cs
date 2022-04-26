using Base.Contracts.BLL;
using BLL.App.Contracts.Services;

namespace BLL.App.Contracts;

public interface IAppBLL : IBLL
{
    IAddressService Addresses { get; }
}