using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs.ParticipantDTOs
{
    public class ParticipantCreateDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserAccountID { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(30)]
        public string? EmergencyContact { get; set; }
    }
}

