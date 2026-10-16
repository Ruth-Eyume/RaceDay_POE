using Microsoft.EntityFrameworkCore;
using RaceDay.API.DTOs.ResultDTOs;
using RaceDay.API.Models;
using RaceDayAPI.Data;

namespace RaceDay.API.Services
{
    public class ResultService
    {
        private readonly RaceDayDBContext _context;

        public ResultService(RaceDayDBContext context)
        {
            _context = context;
        }

        // GET: Retrieve all results.
        public async Task<List<ResultResponseDTO>> GetAllAsync()
        {
            return await _context.Results
                .AsNoTracking()
                .Select(r => new ResultResponseDTO
                {
                    ResultID = r.ResultID,
                    EnrollmentID = r.EnrollmentID,
                    FinishTime = r.FinishTime,
                    Position = r.Position,
                    ResultStatus = r.ResultStatus,
                    RecordedAt = r.RecordedAt
                })
                .ToListAsync();
        }

        // GET: Retrieve one result by ID.
        public async Task<ResultResponseDTO?> GetByIdAsync(int id)
        {
            var result = await _context.Results
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ResultID == id);

            return result == null ? null : MapToDTO(result);
        }

        // POST: Create a new result.
        public async Task<ResultResponseDTO> CreateAsync(
            ResultCreateDTO dto)
        {
            bool enrollmentExists = await _context.Enrollments
                .AnyAsync(e => e.EnrollmentID == dto.EnrollmentID);

            if (!enrollmentExists)
            {
                throw new ArgumentException(
                    "The specified enrolment does not exist.");
            }

            ValidateResult(dto.ResultStatus, dto.Position);

            bool resultExists = await _context.Results
                .AnyAsync(r => r.EnrollmentID == dto.EnrollmentID);

            if (resultExists)
            {
                throw new ArgumentException(
                    "A result already exists for this enrolment.");
            }

            var result = new Result
            {
                EnrollmentID = dto.EnrollmentID,
                FinishTime = dto.FinishTime,
                Position = dto.Position,
                ResultStatus = dto.ResultStatus,
                RecordedAt = DateTime.Now
            };

            _context.Results.Add(result);

            await _context.SaveChangesAsync();

            return MapToDTO(result);
        }

        // PUT: Update an existing result.
        public async Task<ResultResponseDTO?> UpdateAsync(
            int id,
            ResultUpdateDTO dto)
        {
            var result = await _context.Results
                .FirstOrDefaultAsync(r => r.ResultID == id);

            if (result == null)
            {
                return null;
            }

            ValidateResult(dto.ResultStatus, dto.Position);

            result.FinishTime = dto.FinishTime;
            result.Position = dto.Position;
            result.ResultStatus = dto.ResultStatus;

            await _context.SaveChangesAsync();

            return MapToDTO(result);
        }

        // DELETE: Delete a result.
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Results
                .FirstOrDefaultAsync(r => r.ResultID == id);

            if (result == null)
            {
                return false;
            }

            _context.Results.Remove(result);

            await _context.SaveChangesAsync();

            return true;
        }

        // Validate result status and finishing position.
        private static void ValidateResult(string status, int? position)
        {
            string[] validStatuses = { "Finished", "DNF", "DNS" };

            if (string.IsNullOrWhiteSpace(status) ||
                !validStatuses.Contains(
                    status,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "ResultStatus must be Finished, DNF, or DNS.");
            }

            if (position.HasValue && position.Value <= 0)
            {
                throw new ArgumentException(
                    "Position must be greater than zero.");
            }
        }

        // Convert a result model into a response DTO.
        private static ResultResponseDTO MapToDTO(Result result)
        {
            return new ResultResponseDTO
            {
                ResultID = result.ResultID,
                EnrollmentID = result.EnrollmentID,
                FinishTime = result.FinishTime,
                Position = result.Position,
                ResultStatus = result.ResultStatus,
                RecordedAt = result.RecordedAt
            };
        }
    }
}


