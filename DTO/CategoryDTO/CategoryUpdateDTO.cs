using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}