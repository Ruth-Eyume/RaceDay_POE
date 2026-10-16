using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDay.API.DTOs.EnrollmentDTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Services
{
    public class EnrolmentService
    {
        private readonly RaceDayDBContext _context;


    public EnrolmentService(RaceDayDBContext context)
        {
            _context = context;
        }

        // Convert an enrolment model into a response DTO.
        private static EnrollmentResponseDTO MapToResponseDTO(Enrollment e)
        {
            return new EnrollmentResponseDTO
            {
                EnrollmentID = e.EnrollmentID,
                ParticipantID = e.ParticipantID,
                EventCategoryID = e.EventCategoryID,
                EnrollmentDate = e.EnrollmentDate,
                RaceNumber = e.RaceNumber,
                Status = e.Status
            };
        }

        // GET: Retrieve all enrolments.
        public async Task<List<EnrollmentResponseDTO>> GetAllEnrolmentsAsync()
        {
            var enrollments = await _context.Enrollments
                .AsNoTracking()
                .ToListAsync();

            return enrollments.Select(MapToResponseDTO).ToList();
        }

        // GET: Retrieve an enrolment by ID.
        public async Task<EnrollmentResponseDTO?> GetEnrolmentByIdAsync(int id)
        {
            var enrollment = await _context.Enrollments
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);

            return enrollment == null ? null : MapToResponseDTO(enrollment);
        }

        // POST: Create a new enrolment.
        public async Task<EnrollmentResponseDTO> CreateEnrolmentAsync(
            EnrollmentCreateDTO dto)
        {
            // Check that the participant exists.
            bool participantExists = await _context.Participants
                .AnyAsync(p => p.ParticipantID == dto.ParticipantID);

            if (!participantExists)
                throw new ArgumentException("The specified participant does not exist.");

            // Check that the event category exists.
            bool eventCategoryExists = await _context.EventCategories
                .AnyAsync(ec => ec.EventCategoryID == dto.EventCategoryID);

            if (!eventCategoryExists)
                throw new ArgumentException("The specified event category does not exist.");

            // Validate the enrolment status.
            if (!new[] { "Pending", "Confirmed", "Cancelled" }
                .Contains(dto.Status))
            {
                throw new ArgumentException(
                    "Status must be Pending, Confirmed, or Cancelled.");
            }

            // Prevent duplicate enrolments for the same participant and category.
            bool alreadyEnrolled = await _context.Enrollments.AnyAsync(e =>
                e.ParticipantID == dto.ParticipantID &&
                e.EventCategoryID == dto.EventCategoryID);

            if (alreadyEnrolled)
            {
                throw new ArgumentException(
                    "This participant is already enrolled in this event category.");
            }

            // Generate a race number.
            string raceNumber;
            do
            {
                raceNumber = $"RD-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";
            }
            while (await _context.Enrollments.AnyAsync(
                e => e.RaceNumber == raceNumber));

            var enrollment = new Enrollment
            {
                ParticipantID = dto.ParticipantID,
                EventCategoryID = dto.EventCategoryID,
                RaceNumber = raceNumber,
                Status = dto.Status,
                EnrollmentDate = DateTime.Now
            };

            _context.Enrollments.Add(enrollment);

            await _context.SaveChangesAsync();

            return MapToResponseDTO(enrollment);
        }

        // PUT: Update an existing enrolment.
        public async Task<EnrollmentResponseDTO?> UpdateEnrolmentAsync(
            int id, EnrollmentUpdateDTO dto)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);

            if (enrollment == null)
                return null;

            bool participantExists = await _context.Participants
                .AnyAsync(p => p.ParticipantID == dto.ParticipantID);

            if (!participantExists)
                throw new ArgumentException("The specified participant does not exist.");

            bool eventCategoryExists = await _context.EventCategories
                .AnyAsync(ec => ec.EventCategoryID == dto.EventCategoryID);

            if (!eventCategoryExists)
                throw new ArgumentException("The specified event category does not exist.");

            if (!new[] { "Pending", "Confirmed", "Cancelled" }
                .Contains(dto.Status))
            {
                throw new ArgumentException(
                    "Status must be Pending, Confirmed, or Cancelled.");
            }

            bool duplicate = await _context.Enrollments.AnyAsync(e =>
                e.EnrollmentID != id &&
                e.ParticipantID == dto.ParticipantID &&
                e.EventCategoryID == dto.EventCategoryID);

            if (duplicate)
            {
                throw new ArgumentException(
                    "This participant is already enrolled in this event category.");
            }

            enrollment.ParticipantID = dto.ParticipantID;
            enrollment.EventCategoryID = dto.EventCategoryID;
            enrollment.Status = dto.Status;

            await _context.SaveChangesAsync();

            return MapToResponseDTO(enrollment);
        }

        // DELETE: Delete an enrolment.
        public async Task<bool> DeleteEnrolmentAsync(int id)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);

            if (enrollment == null)
                return false;

            _context.Enrollments.Remove(enrollment);

            await _context.SaveChangesAsync();

            return true;
        }
    }


}
