using Microsoft.EntityFrameworkCore;
using RaceDay.API.DTOs.OrganiserDTOs;
using RaceDay.API.Models;
using RaceDayAPI.Data;

namespace RaceDay.API.Services
{
    public class OrganiserService
    {
        private readonly RaceDayDBContext _context;

        public OrganiserService(RaceDayDBContext context)
        {
            _context = context;
        }

        public async Task<List<OrganiserResponseDTO>> GetAllAsync()
        {
            return await _context.Set<Organiser>()
                .Select(o => new OrganiserResponseDTO
                {
                    OrganiserID = o.OrganiserID,
                    UserAccountID = o.UserAccountID,
                    OrganisationName = o.OrganisationName,
                    ContactNumber = o.ContactNumber,
                    CreatedAt = o.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<OrganiserResponseDTO?> GetByIdAsync(int id)
        {
            var organiser = await _context.Set<Organiser>().FindAsync(id);

            if (organiser == null)
                return null;

            return new OrganiserResponseDTO
            {
                OrganiserID = organiser.OrganiserID,
                UserAccountID = organiser.UserAccountID,
                OrganisationName = organiser.OrganisationName,
                ContactNumber = organiser.ContactNumber,
                CreatedAt = organiser.CreatedAt
            };
        }

        public async Task<OrganiserResponseDTO> CreateAsync(
            OrganiserCreateDTO dto)
        {
            var userExists = await _context.Set<UserAccount>()
                .AnyAsync(u => u.UserAccountID == dto.UserAccountID);

            if (!userExists)
                throw new ArgumentException("The specified user account does not exist.");

            var organiser = new Organiser
            {
                UserAccountID = dto.UserAccountID,
                OrganisationName = dto.OrganisationName,
                ContactNumber = dto.ContactNumber,
                CreatedAt = DateTime.Now
            };

            _context.Set<Organiser>().Add(organiser);
            await _context.SaveChangesAsync();

            return new OrganiserResponseDTO
            {
                OrganiserID = organiser.OrganiserID,
                UserAccountID = organiser.UserAccountID,
                OrganisationName = organiser.OrganisationName,
                ContactNumber = organiser.ContactNumber,
                CreatedAt = organiser.CreatedAt
            };
        }

        public async Task<OrganiserResponseDTO?> UpdateAsync(
            int id, OrganiserUpdateDTO dto)
        {
            var organiser = await _context.Set<Organiser>().FindAsync(id);

            if (organiser == null)
                return null;

            var userExists = await _context.Set<UserAccount>()
                .AnyAsync(u => u.UserAccountID == dto.UserAccountID);

            if (!userExists)
                throw new ArgumentException("The specified user account does not exist.");

            organiser.UserAccountID = dto.UserAccountID;
            organiser.OrganisationName = dto.OrganisationName;
            organiser.ContactNumber = dto.ContactNumber;

            await _context.SaveChangesAsync();

            return new OrganiserResponseDTO
            {
                OrganiserID = organiser.OrganiserID,
                UserAccountID = organiser.UserAccountID,
                OrganisationName = organiser.OrganisationName,
                ContactNumber = organiser.ContactNumber,
                CreatedAt = organiser.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var organiser = await _context.Set<Organiser>().FindAsync(id);

            if (organiser == null)
                return false;

            _context.Set<Organiser>().Remove(organiser);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

