using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using DateTime = Domain.NotMapped.DateTime;

namespace Domain
{
    //TODO: Add reference to user
    public class ProductionMeta : DateTime
    {
        public string Line { get; set; } = default!;
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        public SqlDateTime RealStartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        [DataType(DataType.Date)]
        public SqlDateTime RealEndDate { get; set; }

        public Guid SupplyId { get; set; }
        public Supply Supply { get; set; } = default!;

        public ICollection<Production> Productions { get; set; }
        public ICollection<ActiveNotification> ActiveNotifications { get; set; }
    }
}