using System;

namespace MyMeetUp.Logic.Entities
{
    public class Payment:EntityWithDate
    {
        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public string Notes { get; set; }
    }
}
