using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public ICollection<UserTeam>? UserTeams { get; set; }
        public ICollection<UserNotification>? UserNotifications { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Warehouse>? Warehouses { get; set; }
        public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
    }
}