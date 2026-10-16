using Microsoft.AspNetCore.Mvc;
using RaceDay.API.DTOs.OrganiserDTOs;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/organisers")]
    public class OrganisersController : ControllerBase
    {
        private readonly OrganiserService _service;

        public OrganisersController(OrganiserService service)
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
            var organiser = await _service.GetByIdAsync(id);

            if (organiser == null)
                return NotFound();

            return Ok(organiser);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrganiserCreateDTO dto)
        {
            try
            {
                var organiser = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = organiser.OrganiserID },
                    organiser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id, OrganiserUpdateDTO dto)
        {
            try
            {
                var organiser = await _service.UpdateAsync(id, dto);

                if (organiser == null)
                    return NotFound();

                return Ok(organiser);
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

