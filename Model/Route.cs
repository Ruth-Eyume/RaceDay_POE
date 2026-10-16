using System.ComponentModel.DataAnnotations;

namespace RaceDay.API.Models
{
    public class Route
    {
        public int RouteID { get; set; }

    [Required]
        [StringLength(150)]
        public string RouteName { get; set; } = string.Empty;

        [Range(0.01, 9999.99)]
        public decimal DistanceKm { get; set; }

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

