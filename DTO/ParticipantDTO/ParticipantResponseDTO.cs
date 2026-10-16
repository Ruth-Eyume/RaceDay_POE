using System;

namespace RaceDay.API.DTOs.ParticipantDTOs
{
    public class ParticipantResponseDTO
    {
        public int ParticipantID { get; set; }

        public int UserAccountID { get; set; }

        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? EmergencyContact { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

