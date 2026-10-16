using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs.EventCategoryDTOs
{
    public class EventCategoryCreateDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EventID { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryID { get; set; }

        [Range(0.01, 100000)]
        public decimal EntryFee { get; set; }

        [Range(1, int.MaxValue)]
        public int? MaxParticipants { get; set; }
    }
}

