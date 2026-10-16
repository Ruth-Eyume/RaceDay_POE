using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs.OrganiserDTOs
{
    public class OrganiserCreateDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserAccountID { get; set; }

        [Required]
        [StringLength(150)]
        public string OrganisationName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? ContactNumber { get; set; }
    }
}

