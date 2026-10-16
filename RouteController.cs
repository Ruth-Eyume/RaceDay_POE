using Microsoft.AspNetCore.Mvc;
using RaceDay.API.DTOs.RouteDTOs;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly RouteService _routeService;


    public RoutesController(RouteService routeService)
        {
            _routeService = routeService;
        }

        // GET: api/routes
        [HttpGet]
        public async Task<ActionResult<List<RouteResponseDTO>>> GetAllRoutes()
        {
            var routes = await _routeService.GetAllRoutesAsync();
            return Ok(routes);
        }

        // GET: api/routes/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RouteResponseDTO>> GetRouteById(int id)
        {
            var route = await _routeService.GetRouteByIdAsync(id);

            if (route == null)
            {
                return NotFound(new { message = "Route not found." });
            }

            return Ok(route);
        }

        // POST: api/routes
        [HttpPost]
        public async Task<ActionResult<RouteResponseDTO>> CreateRoute(
            [FromBody] RouteCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var route = await _routeService.CreateRouteAsync(dto);

                return CreatedAtAction(
                    nameof(GetRouteById),
                    new { id = route.RouteID },
                    route);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/routes/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RouteResponseDTO>> UpdateRoute(
            int id,
            [FromBody] RouteUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var route = await _routeService.UpdateRouteAsync(id, dto);

                if (route == null)
                {
                    return NotFound(new { message = "Route not found." });
                }

                return Ok(route);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/routes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var deleted = await _routeService.DeleteRouteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Route not found." });
            }

            return NoContent();
        }
    }


}
