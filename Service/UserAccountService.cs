using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.DTOs.UserAccountDTOs;
using RaceDay.API.Models;
using RaceDayAPI.Data;

namespace RaceDay.API.Services
{
    public class UserAccountService
    {
        private readonly RaceDayDBContext _context;
        private readonly PasswordHasher<UserAccount> _passwordHasher = new();

        public UserAccountService(RaceDayDBContext context)
        {
            _context = context;
        }

        public async Task<List<UserAccountResponseDTO>> GetAllAsync()
        {
            return await _context.Set<UserAccount>()
                .Select(u => new UserAccountResponseDTO
                {
                    UserAccountID = u.UserAccountID,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToListAsync();
        }

        public async Task<UserAccountResponseDTO?> GetByIdAsync(int id)
        {
            var user = await _context.Set<UserAccount>().FindAsync(id);

            return user == null ? null : MapToDTO(user);
        }

        public async Task<UserAccountResponseDTO> CreateAsync(
            UserAccountCreateDTO dto)
        {
            bool emailExists = await _context.Set<UserAccount>()
                .AnyAsync(u => u.Email == dto.Email);

            if (emailExists)
                throw new ArgumentException(
                    "An account with this email already exists.");

            var user = new UserAccount
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = dto.Role,
                CreatedAt = DateTime.Now
            };

            user.PasswordHash = _passwordHasher.HashPassword(
                user, dto.Password);

            _context.Set<UserAccount>().Add(user);
            await _context.SaveChangesAsync();

            return MapToDTO(user);
        }

        public async Task<UserAccountResponseDTO?> UpdateAsync(
            int id, UserAccountUpdateDTO dto)
        {
            var user = await _context.Set<UserAccount>().FindAsync(id);

            if (user == null)
                return null;

            bool emailUsed = await _context.Set<UserAccount>()
                .AnyAsync(u => u.Email == dto.Email
                    && u.UserAccountID != id);

            if (emailUsed)
                throw new ArgumentException(
                    "Another account already uses this email.");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Role = dto.Role;

            await _context.SaveChangesAsync();

            return MapToDTO(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Set<UserAccount>().FindAsync(id);

            if (user == null)
                return false;

            _context.Set<UserAccount>().Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        private static UserAccountResponseDTO MapToDTO(UserAccount user)
        {
            return new UserAccountResponseDTO
            {
                UserAccountID = user.UserAccountID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}

