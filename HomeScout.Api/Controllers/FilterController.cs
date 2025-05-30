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
    public class FilterController : ControllerBase
    {
        private readonly IFilterService _filterService;
        private readonly IValidator<CreateFilterDto> _createFilterValidator;
        private readonly IValidator<UpdateFilterDto> _updateFilterValidator;

        public FilterController(IFilterService filterService, IValidator<CreateFilterDto> createFilterValidator, IValidator<UpdateFilterDto> updateFilterValidator)
        {
            _filterService = filterService;
            _createFilterValidator = createFilterValidator;
            _updateFilterValidator = updateFilterValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FilterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FilterDto>>> GetAll()
        {
            var filters = await _filterService.GetAllAsync();
            return Ok(filters);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FilterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FilterDto>> GetById(int id)
        {
            var filter = await _filterService.GetByIdAsync(id);
            if (filter == null)
                return NotFound();

            return Ok(filter);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PagedList<FilterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<FilterDto>>> GetFiltered([FromQuery] FilterParameters parameters)
        {
            var result = await _filterService.GetFilteredAsync(parameters);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(FilterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FilterDto>> Create([FromBody] CreateFilterDto dto)
        {
            var validationResult = await _createFilterValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var created = await _filterService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(FilterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FilterDto>> Update(int id, [FromBody] UpdateFilterDto dto)
        {
            var validationResult = await _updateFilterValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var updated = await _filterService.UpdateAsync(id, dto);
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
            await _filterService.DeleteAsync(id);
            return NoContent();
        }
    }
}
