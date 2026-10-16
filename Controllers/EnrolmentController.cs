using Microsoft.AspNetCore.Mvc;
using RaceDay.API.DTOs.EnrollmentDTOs;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/enrolments")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly EnrolmentService _enrolmentService;

        public EnrolmentsController(EnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        // GET: api/enrolments
        [HttpGet]
        public async Task<ActionResult<List<EnrollmentResponseDTO>>> GetAllEnrolments()
        {
            var enrollments =
                await _enrolmentService.GetAllEnrolmentsAsync();

            return Ok(enrollments);
        }

        // GET: api/enrolments/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EnrollmentResponseDTO>> GetEnrolmentById(
            int id)
        {
            var enrollment =
                await _enrolmentService.GetEnrolmentByIdAsync(id);

            if (enrollment == null)
                return NotFound(new { message = "Enrollment not found." });

            return Ok(enrollment);
        }

        // POST: api/enrolments
        [HttpPost]
        public async Task<ActionResult<EnrollmentResponseDTO>> CreateEnrolment(
            [FromBody] EnrollmentCreateDTO dto)
        {
            var enrollment =
                await _enrolmentService.CreateEnrolmentAsync(dto);

            return CreatedAtAction(
                nameof(GetEnrolmentById),
                new { id = enrollment.EnrollmentID },
                enrollment);
        }

        // PUT: api/enrolments/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<EnrollmentResponseDTO>> UpdateEnrolment(
            int id,
            [FromBody] EnrollmentUpdateDTO dto)
        {
            var enrollment =
                await _enrolmentService.UpdateEnrolmentAsync(id, dto);

            if (enrollment == null)
                return NotFound(new { message = "Enrollment not found." });

            return Ok(enrollment);
        }

        // DELETE: api/enrolments/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEnrolment(int id)
        {
            var deleted =
                await _enrolmentService.DeleteEnrolmentAsync(id);

            if (!deleted)
                return NotFound(new { message = "Enrollment not found." });

            return NoContent();
        }
    }
}