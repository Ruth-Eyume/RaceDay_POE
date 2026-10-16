using Microsoft.EntityFrameworkCore;
using RaceDay.API.DTOs.EventCategoryDTOs;
using RaceDay.API.Models;
using RaceDayAPI.Data;

namespace RaceDay.API.Services
{
    public class EventCategoryService
    {
        private readonly RaceDayDBContext _context;

        public EventCategoryService(RaceDayDBContext context)
        {
            _context = context;
        }

        public async Task<List<EventCategoryResponseDTO>> GetAllAsync()
        {
            return await _context.Set<EventCategory>()
                .Select(ec => new EventCategoryResponseDTO
                {
                    EventCategoryID = ec.EventCategoryID,
                    EventID = ec.EventID,
                    CategoryID = ec.CategoryID,
                    EntryFee = ec.EntryFee,
                    MaxParticipants = ec.MaxParticipants,
                    CreatedAt = ec.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<EventCategoryResponseDTO?> GetByIdAsync(int id)
        {
            var ec = await _context.Set<EventCategory>().FindAsync(id);

            if (ec == null)
                return null;

            return new EventCategoryResponseDTO
            {
                EventCategoryID = ec.EventCategoryID,
                EventID = ec.EventID,
                CategoryID = ec.CategoryID,
                EntryFee = ec.EntryFee,
                MaxParticipants = ec.MaxParticipants,
                CreatedAt = ec.CreatedAt
            };
        }

        public async Task<EventCategoryResponseDTO> CreateAsync(
            EventCategoryCreateDTO dto)
        {
            // Check that the referenced event exists.
            var eventExists = await _context.Set<Event>()
                .AnyAsync(e => e.EventID == dto.EventID);

            if (!eventExists)
                throw new ArgumentException("The specified event does not exist.");

            // Check that the referenced category exists.
            var categoryExists = await _context.Set<Category>()
                .AnyAsync(c => c.CategoryID == dto.CategoryID);

            if (!categoryExists)
                throw new ArgumentException("The specified category does not exist.");

            var ec = new EventCategory
            {
                EventID = dto.EventID,
                CategoryID = dto.CategoryID,
                EntryFee = dto.EntryFee,
                MaxParticipants = dto.MaxParticipants,
                CreatedAt = DateTime.Now
            };

            _context.Set<EventCategory>().Add(ec);
            await _context.SaveChangesAsync();

            return new EventCategoryResponseDTO
            {
                EventCategoryID = ec.EventCategoryID,
                EventID = ec.EventID,
                CategoryID = ec.CategoryID,
                EntryFee = ec.EntryFee,
                MaxParticipants = ec.MaxParticipants,
                CreatedAt = ec.CreatedAt
            };
        }

        public async Task<EventCategoryResponseDTO?> UpdateAsync(
            int id, EventCategoryUpdateDTO dto)
        {
            var ec = await _context.Set<EventCategory>().FindAsync(id);

            if (ec == null)
                return null;

            var eventExists = await _context.Set<Event>()
                .AnyAsync(e => e.EventID == dto.EventID);

            if (!eventExists)
                throw new ArgumentException("The specified event does not exist.");

            var categoryExists = await _context.Set<Category>()
                .AnyAsync(c => c.CategoryID == dto.CategoryID);

            if (!categoryExists)
                throw new ArgumentException("The specified category does not exist.");

            ec.EventID = dto.EventID;
            ec.CategoryID = dto.CategoryID;
            ec.EntryFee = dto.EntryFee;
            ec.MaxParticipants = dto.MaxParticipants;

            await _context.SaveChangesAsync();

            return new EventCategoryResponseDTO
            {
                EventCategoryID = ec.EventCategoryID,
                EventID = ec.EventID,
                CategoryID = ec.CategoryID,
                EntryFee = ec.EntryFee,
                MaxParticipants = ec.MaxParticipants,
                CreatedAt = ec.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ec = await _context.Set<EventCategory>().FindAsync(id);

            if (ec == null)
                return false;

            _context.Set<EventCategory>().Remove(ec);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

