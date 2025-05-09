using FluentValidation;
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
        private readonly IValidator<CreateListingDto> _createListingValidator;
        private readonly IValidator<UpdateListingDto> _updateListingValidator;

        public ListingController(IListingService listingService, IValidator<CreateListingDto> createListingValidator, IValidator<UpdateListingDto> updateListingValidator)
        {
            _listingService = listingService;
            _createListingValidator = createListingValidator;
            _updateListingValidator = updateListingValidator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ListingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ListingDto>>> GetAll()
        {
            var listings = await _listingService.GetAllAsync();
            return Ok(listings);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PagedList<ListingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<ListingDto>>> GetAllPaginated([FromQuery] ListingParameters parameters)
        {
            var listings = await _listingService.GetAllPaginatedAsync(parameters);
            return Ok(listings);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ListingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingDto>> GetById(int id)
        {
            var listing = await _listingService.GetByIdAsync(id);
            if (listing == null)
                return NotFound();

            return Ok(listing);
        }

        [HttpGet("title/{title}")]
        [ProducesResponseType(typeof(IEnumerable<ListingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ListingDto>>> GetByTitle(string title)
        {
            var listings = await _listingService.GetByTitleAsync(title);
            return Ok(listings);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<ListingDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ListingDto>>> GetByUserId(string userId)
        {
            var listings = await _listingService.GetByUserIdAsync(userId);
            return Ok(listings);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ListingDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingDto>> Create([FromBody] CreateListingDto dto)
        {
            var validationResult = await _createListingValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var created = await _listingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ListingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingDto>> Update(int id, [FromBody] UpdateListingDto dto)
        {
            var validationResult = await _updateListingValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var updated = await _listingService.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _listingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
