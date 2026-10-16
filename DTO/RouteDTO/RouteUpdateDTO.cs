using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.DTOs.RouteDTOs
{
    public class RouteUpdateDTO
    {
        [Required]
        [StringLength(150)]
        public string RouteName { get; set; } = string.Empty;

        [Range(0.01, 9999.99)]
        public decimal DistanceKm { get; set; }

        [Range(1, int.MaxValue)]

        [StringLength(150)]
        public string? StartPoint { get; set; }

        [StringLength(150)]
        public string? EndPoint { get; set; }

        [StringLength(500)]
        public string? RouteDescription { get; set; }

        [StringLength(500)]
        public string? MapUrl { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
    }
}

