using Microsoft.EntityFrameworkCore;
using RaceDay.API.DTOs.ParticipantDTOs;
using RaceDay.API.Models;
using RaceDayAPI.Data;

namespace RaceDay.API.Services
{
    public class ParticipantService
    {
        private readonly RaceDayDBContext _context;

        public ParticipantService(RaceDayDBContext context)
        {
            _context = context;
        }

        public async Task<List<ParticipantResponseDTO>> GetAllAsync()
        {
            return await _context.Set<Participant>()
                .Select(p => new ParticipantResponseDTO
                {
                    ParticipantID = p.ParticipantID,
                    UserAccountID = p.UserAccountID,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    EmergencyContact = p.EmergencyContact,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ParticipantResponseDTO?> GetByIdAsync(int id)
        {
            var participant = await _context.Set<Participant>().FindAsync(id);

            if (participant == null)
                return null;

            return MapToDTO(participant);
        }

        public async Task<ParticipantResponseDTO> CreateAsync(
            ParticipantCreateDTO dto)
        {
            var userExists = await _context.Set<UserAccount>()
                .AnyAsync(u => u.UserAccountID == dto.UserAccountID);

            if (!userExists)
                throw new ArgumentException("The specified user account does not exist.");

            var participant = new Participant
            {
                UserAccountID = dto.UserAccountID,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                EmergencyContact = dto.EmergencyContact,
                CreatedAt = DateTime.Now
            };

            _context.Set<Participant>().Add(participant);
            await _context.SaveChangesAsync();

            return MapToDTO(participant);
        }

        public async Task<ParticipantResponseDTO?> UpdateAsync(
            int id, ParticipantUpdateDTO dto)
        {
            var participant = await _context.Set<Participant>().FindAsync(id);

            if (participant == null)
                return null;

            var userExists = await _context.Set<UserAccount>()
                .AnyAsync(u => u.UserAccountID == dto.UserAccountID);

            if (!userExists)
                throw new ArgumentException("The specified user account does not exist.");

            participant.UserAccountID = dto.UserAccountID;
            participant.Gender = dto.Gender;
            participant.DateOfBirth = dto.DateOfBirth;
            participant.EmergencyContact = dto.EmergencyContact;

            await _context.SaveChangesAsync();

            return MapToDTO(participant);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var participant = await _context.Set<Participant>().FindAsync(id);

            if (participant == null)
                return false;

            _context.Set<Participant>().Remove(participant);
            await _context.SaveChangesAsync();

            return true;
        }

        private static ParticipantResponseDTO MapToDTO(Participant participant)
        {
            return new ParticipantResponseDTO
            {
                ParticipantID = participant.ParticipantID,
                UserAccountID = participant.UserAccountID,
                Gender = participant.Gender,
                DateOfBirth = participant.DateOfBirth,
                EmergencyContact = participant.EmergencyContact,
                CreatedAt = participant.CreatedAt
            };
        }
    }
}

