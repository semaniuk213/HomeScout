using FluentValidation;
using HomeScout.BLL.DTOs;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeScout.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ListingFilterController : ControllerBase
    {
        private readonly IListingFilterService _listingFilterService;
        private readonly IValidator<CreateListingFilterDto> _createListingFilterValidator;
        private readonly IValidator<UpdateListingFilterDto> _updateListingFilterValidator;

        public ListingFilterController(IListingFilterService listingFilterService, IValidator<CreateListingFilterDto> createListingFilterValidator, IValidator<UpdateListingFilterDto> updateListingFilterValidator)
        {
            _listingFilterService = listingFilterService;
            _createListingFilterValidator = createListingFilterValidator;
            _updateListingFilterValidator = updateListingFilterValidator;
        }

        [HttpGet("listing/{listingId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ListingFilterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ListingFilterDto>>> GetByListingId(int listingId)
        {
            var filters = await _listingFilterService.GetByListingIdAsync(listingId);
            return Ok(filters);
        }

        [HttpGet("filter/{filterId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ListingFilterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ListingFilterDto>>> GetByFilterId(int filterId)
        {
            var filters = await _listingFilterService.GetByFilterIdAsync(filterId);
            return Ok(filters);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PagedList<ListingFilterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<ListingFilterDto>>> GetAllPaginated([FromQuery] ListingFilterParameters parameters)
        {
            var listingFilters = await _listingFilterService.GetAllPaginatedAsync(parameters);
            return Ok(listingFilters);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ListingFilterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingFilterDto>> Create([FromBody] CreateListingFilterDto dto)
        {
            var validationResult = await _createListingFilterValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var created = await _listingFilterService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByListingId), new { listingId = created.ListingId }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ListingFilterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListingFilterDto>> Update(int id, [FromBody] UpdateListingFilterDto dto)
        {
            var validationResult = await _updateListingFilterValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var updated = await _listingFilterService.UpdateAsync(id, dto);
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
            await _listingFilterService.DeleteAsync(id);
            return NoContent();
        }
    }
}
