using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs.UserAccountDTOs
{
    public class UserAccountUpdateDTO
    {
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
        [RegularExpression("^(Organizer|Participant)$",
            ErrorMessage = "Role must be Organizer or Participant.")]
        public string Role { get; set; } = "Participant";
    }
}

