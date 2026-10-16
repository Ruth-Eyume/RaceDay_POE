using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Organiser
    {
        public int OrganiserID { get; set; }

        [Required]
        public int UserAccountID { get; set; }

        [Required]
        [StringLength(150)]
        public string OrganisationName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? ContactNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationship with the user account
        public UserAccount? UserAccount { get; set; }
    }
}

