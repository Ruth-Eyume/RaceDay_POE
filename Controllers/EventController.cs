using Microsoft.AspNetCore.Mvc;
using RaceDay.API.DTOs.EventDTOs;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/events
        [HttpGet]
        public async Task<ActionResult<List<EventResponseDTO>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();

            return Ok(events);
        }

        // GET: api/events/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EventResponseDTO>> GetEventById(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);

            if (eventItem == null)
                return NotFound(new { message = "Event not found." });

            return Ok(eventItem);
        }

        // POST: api/events
        [HttpPost]
        public async Task<ActionResult<EventResponseDTO>> CreateEvent(
            [FromBody] EventCreateDTO dto)
        {
            var createdEvent = await _eventService.CreateEventAsync(dto);

            return CreatedAtAction(
                nameof(GetEventById),
                new { id = createdEvent.EventID },
                createdEvent);
        }

        // PUT: api/events/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<EventResponseDTO>> UpdateEvent(
            int id, [FromBody] EventUpdateDTO dto)
        {
            var updatedEvent = await _eventService.UpdateEventAsync(id, dto);

            if (updatedEvent == null)
                return NotFound(new { message = "Event not found." });

            return Ok(updatedEvent);
        }

        // DELETE: api/events/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var deleted = await _eventService.DeleteEventAsync(id);

            if (!deleted)
                return NotFound(new { message = "Event not found." });

            return NoContent();
        }
    }
}
