using System;

namespace RaceDay.API.DTOs.EnrollmentDTOs
{
    public class EnrollmentResponseDTO
    {
        public int EnrollmentID { get; set; }

        public int ParticipantID { get; set; }

        public int EventCategoryID { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string RaceNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}