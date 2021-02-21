using Domain.NotMapped;
using Microsoft.VisualBasic;

namespace Domain
{
    public class ActiveNotification : BaseIdentity
    {
        public string Head { get; set; }
        public string Info { get; set; }
    }
}