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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateUserDto> _createUserValidator;
        private readonly IValidator<UpdateUserDto> _updateUserValidator;

        public UserController(IUserService userService, IValidator<CreateUserDto> createUserValidator, IValidator<UpdateUserDto> updateUserValidator)
        {
            _userService = userService;
            _createUserValidator = createUserValidator;
            _updateUserValidator = updateUserValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PagedList<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedList<UserDto>>> GetAllPaginated([FromQuery] UserParameters parameters)
        {
            var users = await _userService.GetAllPaginatedAsync(parameters);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            var validationResult = await _createUserValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var created = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto dto)
        {
            var validationResult = await _updateUserValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var updated = await _userService.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
