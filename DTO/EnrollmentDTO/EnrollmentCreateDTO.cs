using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs.EnrollmentDTOs
{
    public class EnrollmentCreateDTO
    {
        [Range(1, int.MaxValue)]
        public int ParticipantID { get; set; }

        [Range(1, int.MaxValue)]
        public int EventCategoryID { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = "Registered";
    }
}