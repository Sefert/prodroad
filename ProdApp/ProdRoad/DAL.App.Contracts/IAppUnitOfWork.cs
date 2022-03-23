#nullable enable
using Base.Contracts.DAL;

namespace DAL.App.Contracts;

public interface IAppUnitOfWork : IUnitOfWork
{
    IAddressRepository Addresses { get; }
}