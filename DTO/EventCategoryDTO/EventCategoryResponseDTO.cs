using System;

namespace RaceDay.API.DTOs.EventCategoryDTOs
{
    public class EventCategoryResponseDTO
    {
        public int EventCategoryID { get; set; }

        public int EventID { get; set; }

        public int CategoryID { get; set; }

        public decimal EntryFee { get; set; }

  

        public int? MaxParticipants { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

