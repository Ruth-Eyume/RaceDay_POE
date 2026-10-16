using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Event
    {
        public int EventID { get; set; }

        [Required]
        public int OrganiserID { get; set; }

        [Required]
        public int RouteID { get; set; }

        [Required]
        [StringLength(150)]
        public string EventName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime RegistrationDeadline { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Draft";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public Organiser? Organiser { get; set; }

        public Route? Route { get; set; }

        public List<EventCategory> EventCategories { get; set; }
            = new List<EventCategory>();
    }
}
