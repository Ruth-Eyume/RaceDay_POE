using Microsoft.AspNetCore.Mvc;
using RaceDay.API.DTOs.ParticipantDTOs;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly ParticipantService _service;

        public ParticipantsController(ParticipantService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var participant = await _service.GetByIdAsync(id);

            if (participant == null)
                return NotFound();

            return Ok(participant);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParticipantCreateDTO dto)
        {
            try
            {
                var participant = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = participant.ParticipantID },
                    participant);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, ParticipantUpdateDTO dto)
        {
            try
            {
                var participant = await _service.UpdateAsync(id, dto);

                if (participant == null)
                    return NotFound();

                return Ok(participant);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

