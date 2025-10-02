using HomeScout.ListingService.BLL.DTOs;
using HomeScout.ListingService.BLL.Services.Interfaces;
using HomeScout.ListingService.DAL.Helpers;
using HomeScout.ListingService.DAL.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace HomeScout.ListingService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingFilterController : ControllerBase
    {
        private readonly IListingFilterService _listingFilterService;

        public ListingFilterController(IListingFilterService listingFilterService)
        {
            _listingFilterService = listingFilterService;
        }

        /// <summary>
        /// Retrieves all listing filters with pagination and optional filtering
        /// </summary>
        /// <param name="parameters">ListingFilter filter and pagination parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paged list of listing filters</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<ListingFilterDto>>> GetAll([FromQuery] ListingFilterParameters parameters, CancellationToken cancellationToken)
        {
            var listingFilters = await _listingFilterService.GetAllAsync(parameters, cancellationToken);
            return Ok(listingFilters);
        }

        /// <summary>
        /// Creates a new listing filter
        /// </summary>
        /// <param name="dto">ListingFilter creation data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created listing filter</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingFilterDto>> Create([FromBody] CreateListingFilterDto dto, CancellationToken cancellationToken)
        {
            var created = await _listingFilterService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { listingId = created.ListingId }, created);
        }

        /// <summary>
        /// Updates an existing listing filter
        /// </summary>
        /// <param name="id">ListingFilter ID</param>
        /// <param name="dto">ListingFilter update data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated listing filter</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingFilterDto>> Update(int id, [FromBody] UpdateListingFilterDto dto, CancellationToken cancellationToken)
        {
            var updated = await _listingFilterService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Deletes a listing filter by its ID
        /// </summary>
        /// <param name="id">ListingFilter ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _listingFilterService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}