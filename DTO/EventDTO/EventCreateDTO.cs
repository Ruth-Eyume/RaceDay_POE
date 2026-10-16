using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs.EventDTOs
{
    public class EventCreateDTO
    {
        [Range(1, int.MaxValue)]
        public int OrganiserID { get; set; }

        [Range(1, int.MaxValue)]
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
    }
}