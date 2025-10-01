using HomeScout.BLL.DTOs;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace HomeScout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Retrieves all photos with pagination and optional filtering
        /// </summary>
        /// <param name="parameters">Photo filter and pagination parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paged list of photos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<PhotoDto>>> GetAll([FromQuery] PhotoParameters parameters, CancellationToken cancellationToken)
        {
            var photos = await _photoService.GetAllAsync(parameters, cancellationToken);
            return Ok(photos);
        }

        /// <summary>
        /// Retrieves a photo by its ID
        /// </summary>
        /// <param name="id">Photo ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Photo details</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PhotoDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var photo = await _photoService.GetByIdAsync(id, cancellationToken);
            return Ok(photo);
        }

        /// <summary>
        /// Creates a new photo
        /// </summary>
        /// <param name="dto">Photo creation data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created photo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PhotoDto>> Create([FromBody] CreatePhotoDto dto, CancellationToken cancellationToken)
        {
            var created = await _photoService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing photo
        /// </summary>
        /// <param name="id">Photo ID</param>
        /// <param name="dto">Photo update data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated photo</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PhotoDto>> Update(int id, [FromBody] UpdatePhotoDto dto, CancellationToken cancellationToken)
        {
            var updated = await _photoService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Deletes a photo by its ID
        /// </summary>
        /// <param name="id">Photo ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _photoService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}