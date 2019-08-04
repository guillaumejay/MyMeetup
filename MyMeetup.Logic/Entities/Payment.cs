using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetUp.Logic.Entities
{
    [Table("Payments")]
    public class Payment: EntityWithDate.EntityWithDateTyped
    {
     
        [Column(TypeName = "Date")]
        
        public DateTime PaymentDate { get; set; }

        [Range(1,100)]
        public decimal AmountPaid { get; set; }

        [ForeignKey(nameof(MyMeetupUser))]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]

        public MyMeetupUser User { get; set; }
        [StringLength(5000)]
        public string Notes { get; set; }

    }
}
