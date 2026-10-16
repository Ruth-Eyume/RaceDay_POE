using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDay.API.DTOs.EventDTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Services
{
    public class EventService
    {
        private readonly RaceDayDBContext _context;

        public EventService(RaceDayDBContext context)
        {
            _context = context;
        }

        // Convert an Event model into a response DTO.
        private static EventResponseDTO MapToResponseDTO(Event e)
        {
            return new EventResponseDTO
            {
                EventID = e.EventID,
                OrganiserID = e.OrganiserID,
                RouteID = e.RouteID,
                EventName = e.EventName,
                Description = e.Description,
                EventDate = e.EventDate,
                Location = e.Location,
                RegistrationDeadline = e.RegistrationDeadline,
                Status = e.Status,
                CreatedAt = e.CreatedAt
            };
        }

        // GET: Retrieve all events.
        public async Task<List<EventResponseDTO>> GetAllEventsAsync()
        {
            var events = await _context.Set<Event>()
                .AsNoTracking()
                .ToListAsync();

            return events.Select(MapToResponseDTO).ToList();
        }

        // GET: Retrieve one event by its ID.
        public async Task<EventResponseDTO?> GetEventByIdAsync(int id)
        {
            var e = await _context.Set<Event>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (e == null)
                return null;

            return MapToResponseDTO(e);
        }

        // POST: Create a new event.
        public async Task<EventResponseDTO> CreateEventAsync(
            EventCreateDTO dto)
        {
            var e = new Event
            {
                OrganiserID = dto.OrganiserID,
                RouteID = dto.RouteID,
                EventName = dto.EventName,
                Description = dto.Description,
                EventDate = dto.EventDate,
                Location = dto.Location,
                RegistrationDeadline = dto.RegistrationDeadline,
                Status = dto.Status
            };

            _context.Set<Event>().Add(e);

            await _context.SaveChangesAsync();

            return MapToResponseDTO(e);
        }

        // PUT: Update an existing event.
        public async Task<EventResponseDTO?> UpdateEventAsync(
            int id, EventUpdateDTO dto)
        {
            var e = await _context.Set<Event>()
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (e == null)
                return null;

            e.OrganiserID = dto.OrganiserID;
            e.RouteID = dto.RouteID;
            e.EventName = dto.EventName;
            e.Description = dto.Description;
            e.EventDate = dto.EventDate;
            e.Location = dto.Location;
            e.RegistrationDeadline = dto.RegistrationDeadline;
            e.Status = dto.Status;

            await _context.SaveChangesAsync();

            return MapToResponseDTO(e);
        }

        // DELETE: Delete an existing event.
        public async Task<bool> DeleteEventAsync(int id)
        {
            var e = await _context.Set<Event>()
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (e == null)
                return false;

            _context.Set<Event>().Remove(e);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
