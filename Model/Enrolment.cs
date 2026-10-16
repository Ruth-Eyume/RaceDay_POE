using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }


    [Required]
        public int ParticipantID { get; set; }

        [Required]
        public int EventCategoryID { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [Required]
        [StringLength(20)]
        public string RaceNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        // Navigation properties
        public Participant? Participant { get; set; }

        public EventCategory? EventCategory { get; set; }

        public Result? Result { get; set; }
    }


}
