using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Participant
    {
        public int ParticipantID { get; set; }

        [Required]
        public int UserAccountID { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(30)]
        public string? EmergencyContact { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationship with the user account
        public UserAccount? UserAccount { get; set; }
    }
}

