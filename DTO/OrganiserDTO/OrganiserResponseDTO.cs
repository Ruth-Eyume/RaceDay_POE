using System;

namespace RaceDay.API.DTOs.OrganiserDTOs
{
    public class OrganiserResponseDTO
    {
        public int OrganiserID { get; set; }

        public int UserAccountID { get; set; }

        public string OrganisationName { get; set; } = string.Empty;

        public string? ContactNumber { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

