using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class UserAccount
    {
        public int UserAccountID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "Participant";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relationships
        public Organiser? Organiser { get; set; }

        public Participant? Participant { get; set; }
    }
}


