using System;

namespace RaceDay.API.DTOs.ResultDTOs
{
    public class ResultResponseDTO
    {
        public int ResultID { get; set; }

        public int EnrollmentID { get; set; }

        public TimeSpan? FinishTime { get; set; }

        public int? Position { get; set; }

        public string ResultStatus { get; set; } = string.Empty;

        public DateTime RecordedAt { get; set; }
    }
}

