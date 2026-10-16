using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class EventCategory
    {
        public int EventCategoryID { get; set; }


    [Required]
        public int EventID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Range(0, 100000)]
        public decimal EntryFee { get; set; }

        public int? MaxParticipants { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Event? Event { get; set; }

        public Category? Category { get; set; }

        public List<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();
    }


}
