using System;
using Contracts.DAL.BAse.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IActiveNotificationRepo : IBaseRepository<ActiveNotification>
    {
    }
}