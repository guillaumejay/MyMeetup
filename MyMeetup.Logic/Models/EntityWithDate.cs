using System;
using System.ComponentModel.DataAnnotations;

namespace MyMeetUp.Logic.Modeles
{
    public abstract class EntityWithDate
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt
        {
            get; set;
        }

        public DateTime UpdatedAt { get; set; }

        protected EntityWithDate()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }
    }
}
