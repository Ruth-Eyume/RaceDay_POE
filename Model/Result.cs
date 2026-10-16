using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Result
    {
        public int ResultID { get; set; }


    [Required]
        public int EnrollmentID { get; set; }

        public TimeSpan? FinishTime { get; set; }

        public int? Position { get; set; }

        [Required]
        [StringLength(20)]
        public string ResultStatus { get; set; } = "Finished";

        public DateTime RecordedAt { get; set; }

        // Navigation property
        public Enrollment? Enrollment { get; set; }
    }


}

