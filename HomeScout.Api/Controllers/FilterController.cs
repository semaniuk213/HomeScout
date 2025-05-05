using HomeScout.BLL.DTOs;
using HomeScout.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeScout.Api.Controllers
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FilterDto>>> GetAll()
        {
            var filters = await _filterService.GetAllAsync();
            return Ok(filters);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(FilterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilterDto>> GetById(int id)
        {
            var filter = await _filterService.GetByIdAsync(id);
            if (filter == null)
                return NotFound();

            return Ok(filter);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FilterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FilterDto>> Create([FromBody] CreateFilterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _filterService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut]
        [ProducesResponseType(typeof(FilterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FilterDto>> Update([FromBody] UpdateFilterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _filterService.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _filterService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
