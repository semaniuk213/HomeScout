using HomeScout.BLL.DTOs;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace HomeScout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        /// <summary>
        /// Retrieves all listings with pagination and optional filtering
        /// </summary>
        /// <param name="parameters">Listing filter and pagination parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paged list of listings</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<ListingDto>>> GetAll([FromQuery] ListingParameters parameters, CancellationToken cancellationToken)
        {
            var listings = await _listingService.GetAllAsync(parameters, cancellationToken);
            return Ok(listings);
        }

        /// <summary>
        /// Retrieves a listing by its ID
        /// </summary>
        /// <param name="id">Listing ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Listing details</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var listing = await _listingService.GetByIdAsync(id, cancellationToken);
            return Ok(listing);
        }

        /// <summary>
        /// Creates a new listing
        /// </summary>
        /// <param name="dto">Listing creation data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Created listing</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingDto>> Create([FromBody] CreateListingDto dto, CancellationToken cancellationToken)
        {
            var created = await _listingService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing listing
        /// </summary>
        /// <param name="id">Listing ID</param>
        /// <param name="dto">Listing update data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated listing</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingDto>> Update(int id, [FromBody] UpdateListingDto dto, CancellationToken cancellationToken)
        {
            var updated = await _listingService.UpdateAsync(id, dto, cancellationToken);
            return Ok(updated);
        }

        /// <summary>
        /// Deletes a listing by its ID
        /// </summary>
        /// <param name="id">Listing ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _listingService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}