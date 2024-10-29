using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using StreetHub.Models;
using StreetHub.Services;

namespace StreetHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetController : ControllerBase
    {
        private readonly IStreetService _service; 

        /// <summary>
        /// Initializes a new instance of the <see cref="StreetController"/> class.
        /// </summary>
        /// <param name="service">The service used to manage street operations.</param>
        public StreetController(IStreetService service)
        {
            _service = service; 
        }

        /// <summary>
        /// Creates a new street.
        /// </summary>
        /// <param name="street">The street to create.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation, 
        /// with a result of <see cref="IActionResult"/> indicating the outcome.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateStreet([FromBody] Street street)
        {
            await _service.CreateStreetAsync(street); 
            return CreatedAtAction(nameof(GetStreet), new { id = street.Id }, street); 
        }

        /// <summary>
        /// Retrieves a street by its ID.
        /// </summary>
        /// <param name="id">The ID of the street to retrieve.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation, 
        /// with a result of <see cref="IActionResult"/> containing the street data or a 404 Not Found response.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStreet(int id)
        {
            var street = await _service.GetStreetByIdAsync(id); 
            if (street == null) return NotFound(); 
            return Ok(street); 
        }

        /// <summary>
        /// Deletes a street by its ID.
        /// </summary>
        /// <param name="id">The ID of the street to delete.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation, 
        /// with a result of <see cref="IActionResult"/> indicating the outcome.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStreet(int id)
        {
            await _service.DeleteStreetAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Adds a point to the geometry of a street.
        /// </summary>
        /// <param name="id">The ID of the street to which the point will be added.</param>
        /// <param name="point">The point to add to the street's geometry.</param>
        /// <param name="addToEnd">Indicates whether to add the point to the end of the geometry.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation, 
        /// with a result of <see cref="IActionResult"/> indicating the outcome.</returns>
        [HttpPost("{id}/geometry")]
        public async Task<IActionResult> AddPointToGeometry(int id, [FromBody] Point point, [FromQuery] bool addToEnd)
        {
            var result = await _service.AddPointToStreetGeometryAsync(id, point, addToEnd);
            if (!result) return NotFound();
            return Ok();
        }
    }
}