using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Domain.NotMapped
{
    [NotMapped]
    public class Time : BaseIdentity
    {
        [DataType(DataType.Date)]
        public SqlDateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        public SqlDateTime EndTime { get; set; }
    }
}