using Microsoft.EntityFrameworkCore;
using RaceDay.API.DTOs.RouteDTOs;
using RaceDayAPI.Data;
using RaceDayRoute = RaceDay.API.Models.Route;

namespace RaceDay.API.Services
{
    public class RouteService
    {
        private readonly RaceDayDBContext _context;


    public RouteService(RaceDayDBContext context)
        {
            _context = context;
        }

        // Get all routes.
        public async Task<List<RouteResponseDTO>> GetAllRoutesAsync()
        {
            var routes = await _context.Routes
                .AsNoTracking()
                .ToListAsync();

            return routes.Select(MapToDTO).ToList();
        }

        // Get a route by ID.
        public async Task<RouteResponseDTO?> GetRouteByIdAsync(int id)
        {
            var route = await _context.Routes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RouteID == id);

            return route == null ? null : MapToDTO(route);
        }

        // Create a route.
        public async Task<RouteResponseDTO> CreateRouteAsync(
            RouteCreateDTO dto)
        {
            var route = new RaceDayRoute
            {
                RouteName = dto.RouteName,
                DistanceKm = dto.DistanceKm,
                StartPoint = dto.StartPoint,
                EndPoint = dto.EndPoint,
                RouteDescription = dto.RouteDescription,
                MapUrl = dto.MapUrl,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };

            _context.Routes.Add(route);

            await _context.SaveChangesAsync();

            return MapToDTO(route);
        }

        // Update a route.
        public async Task<RouteResponseDTO?> UpdateRouteAsync(
            int id, RouteUpdateDTO dto)
        {
            var route = await _context.Routes
                .FirstOrDefaultAsync(r => r.RouteID == id);

            if (route == null)
                return null;

            route.RouteName = dto.RouteName;
            route.DistanceKm = dto.DistanceKm;
            route.StartPoint = dto.StartPoint;
            route.EndPoint = dto.EndPoint;
            route.RouteDescription = dto.RouteDescription;
            route.MapUrl = dto.MapUrl;
            route.Latitude = dto.Latitude;
            route.Longitude = dto.Longitude;

            await _context.SaveChangesAsync();

            return MapToDTO(route);
        }

        // Delete a route.
        public async Task<bool> DeleteRouteAsync(int id)
        {
            var route = await _context.Routes
                .FirstOrDefaultAsync(r => r.RouteID == id);

            if (route == null)
                return false;

            _context.Routes.Remove(route);

            await _context.SaveChangesAsync();

            return true;
        }

        // Convert a Route model into a response DTO.
        private static RouteResponseDTO MapToDTO(RaceDayRoute route)
        {
            return new RouteResponseDTO
            {
                RouteID = route.RouteID,
                RouteName = route.RouteName,
                DistanceKm = route.DistanceKm,
                StartPoint = route.StartPoint,
                EndPoint = route.EndPoint,
                RouteDescription = route.RouteDescription,
                MapUrl = route.MapUrl,
                Latitude = route.Latitude,
                Longitude = route.Longitude
            };
        }
    }


}

