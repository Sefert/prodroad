using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    /*TODO: Change to non nullable, too much hassle to do for mvc razor*/
    public class ApplicationUser /*: BaseIdentity*/ : IdentityUser<Guid>, IDomainEntityId
    {
        [MaxLength(30)] public string? FirstName { get; set; }
        [MaxLength(30)] public string? LastName { get; set; }

        public ICollection<UserTeam>? UserTeams { get; set; }
        public ICollection<UserNotification>? UserNotifications { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Warehouse>? Warehouses { get; set; }
        public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
    }
}