using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMeetUp.Logic.Entities
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

        /// <summary>
        /// Bug : columns were not typed on EntityWithDate
        /// </summary>
        public abstract class EntityWithDateTyped
        {
            [Key] public int Id { get; set; }
            [Column(TypeName = "Date")] public DateTime CreatedAt { get; set; }
            [Column(TypeName = "Date")] public DateTime UpdatedAt { get; set; }

            protected EntityWithDateTyped()
            {
                CreatedAt = DateTime.UtcNow;
                UpdatedAt = CreatedAt;
            }
        }
    }
    }
