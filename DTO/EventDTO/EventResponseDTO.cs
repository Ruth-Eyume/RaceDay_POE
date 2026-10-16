using System;

namespace RaceDay.API.DTOs.EventDTOs
{
    public class EventResponseDTO
    {
        public int EventID { get; set; }

        public int OrganiserID { get; set; }

        public int RouteID { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime EventDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public DateTime RegistrationDeadline { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}