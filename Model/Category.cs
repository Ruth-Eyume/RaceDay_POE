using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Navigation property
        public List<EventCategory> EventCategories { get; set; }
            = new List<EventCategory>();
    }
}