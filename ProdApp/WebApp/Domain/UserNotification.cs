
using Domain.NotMapped;

namespace Domain
{
    public class UserNotification : BaseIdentity
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}