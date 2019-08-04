using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetUp.Logic.Entities
{
    [Table("Payments")]
    public class Payment
    {
        [Key]

        public int Id { get; set; }
        [Column(TypeName = "Date")]
        
        public DateTime PaymentDate { get; set; }

        public decimal AmountPaid { get; set; }

        [ForeignKey(nameof(MyMeetupUser))]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]

        public MyMeetupUser User { get; set; }
        [StringLength(5000)]
        public string Notes { get; set; }

    }
}
