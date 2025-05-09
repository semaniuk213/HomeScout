using FluentValidation;
using HomeScout.BLL.DTOs;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Helpers;
using HomeScout.DAL.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeScout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IValidator<CreatePhotoDto> _createPhotoValidator;
        private readonly IValidator<UpdatePhotoDto> _updatePhotoValidator;

        public PhotoController(IPhotoService photoService, IValidator<CreatePhotoDto> createPhotoValidator, IValidator<UpdatePhotoDto> updatePhotoValidator)
        {
            _photoService = photoService;
            _createPhotoValidator = createPhotoValidator;
            _updatePhotoValidator = updatePhotoValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PhotoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetAll()
        {
            var photos = await _photoService.GetAllAsync();
            return Ok(photos);
        }


        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PagedList<PhotoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<PhotoDto>>> GetAllPaginated([FromQuery] PhotoParameters parameters)
        {
            var photos = await _photoService.GetAllPaginatedAsync(parameters);
            return Ok(photos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PhotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PhotoDto>> GetById(int id)
        {
            var photo = await _photoService.GetByIdAsync(id);
            if (photo == null)
                return NotFound();

            return Ok(photo);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PhotoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PhotoDto>> Create([FromBody] CreatePhotoDto dto)
        {
            var validationResult = await _createPhotoValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var created = await _photoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PhotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PhotoDto>> Update(int id, [FromBody] UpdatePhotoDto dto)
        {
            var validationResult = await _updatePhotoValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var updated = await _photoService.UpdateAsync(id, dto);
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
            await _photoService.DeleteAsync(id);
            return NoContent();
        }
    }
}
