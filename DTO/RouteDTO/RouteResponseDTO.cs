namespace RaceDay.API.DTOs.RouteDTOs
{
    public class RouteResponseDTO
    {
        public int RouteID { get; set; }

        public string RouteName { get; set; } = string.Empty;

        public decimal DistanceKm { get; set; }


        public string? StartPoint { get; set; }

        public string? EndPoint { get; set; }

        public string? RouteDescription { get; set; }

        public string? MapUrl { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
    }
}


