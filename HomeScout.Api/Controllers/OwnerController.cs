using HomeScout.BLL.DTOs;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace HomeScout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        /// <summary>
        /// Retrieves all owners with pagination and optional filtering
        /// </summary>
        /// <param name="parameters">Owner filter and pagination parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paged list of owners</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<OwnerDto>>> GetAll([FromQuery] OwnerParameters parameters, CancellationToken cancellationToken)
        {
            var owners = await _ownerService.GetAllAsync(parameters, cancellationToken);
            return Ok(owners);
        }

        /// <summary>
        /// Retrieves an owner by its ID
        /// </summary>
        /// <param name="id">Owner ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Owner details</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OwnerDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var owner = await _ownerService.GetByIdAsync(id, cancellationToken);
            return Ok(owner);
        }

        /// <summary>
        /// Creates a new owner
        /// </summary>
        /// <param name="dto">Owner creation data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created owner</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OwnerDto>> Create([FromBody] CreateOwnerDto dto, CancellationToken cancellationToken)
        {
            var created = await _ownerService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing owner
        /// </summary>
        /// <param name="id">Owner ID</param>
        /// <param name="dto">Owner update data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated owner</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OwnerDto>> Update(int id, [FromBody] UpdateOwnerDto dto, CancellationToken cancellationToken)
        {
            var updated = await _ownerService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Deletes an owner by its ID
        /// </summary>
        /// <param name="id">Owner ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _ownerService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}