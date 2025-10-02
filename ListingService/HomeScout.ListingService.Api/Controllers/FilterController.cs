using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.BLL.Services.Interfaces;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace HomeScout.ListingService.ListingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IFilterService _filterService;

        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }

        /// <summary>
        /// Retrieves all filters with pagination and optional filtering
        /// </summary>
        /// <param name="parameters">Filter and pagination parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paged list of filters</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<FilterDto>>> GetAll([FromQuery] FilterParameters parameters, CancellationToken cancellationToken)
        {
            var result = await _filterService.GetAllAsync(parameters, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a filter by its ID
        /// </summary>
        /// <param name="id">Filter ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Filter details</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FilterDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var filter = await _filterService.GetByIdAsync(id, cancellationToken);
            return Ok(filter);
        }

        /// <summary>
        /// Creates a new filter
        /// </summary>
        /// <param name="dto">Filter creation data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created filter</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FilterDto>> Create([FromBody] CreateFilterDto dto, CancellationToken cancellationToken)
        {
            var created = await _filterService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing filter
        /// </summary>
        /// <param name="id">Filter ID</param>
        /// <param name="dto">Filter update data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated filter</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FilterDto>> Update(int id, [FromBody] UpdateFilterDto dto, CancellationToken cancellationToken)
        {
            var updated = await _filterService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Deletes a filter by its ID
        /// </summary>
        /// <param name="id">Filter ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _filterService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}