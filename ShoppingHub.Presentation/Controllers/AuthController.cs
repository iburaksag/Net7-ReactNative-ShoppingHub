using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Users.Commands.CreateUser;
using ShoppingHub.Application.Users.Commands.LoginUser;

namespace ShoppingHub.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var createUserCommand = new CreateUserCommand(registerDto);
            var authResult = await _mediator.Send(createUserCommand);

            if (!authResult.Success)
            {
                return BadRequest(new { Message = authResult.Message, Errors = authResult.Errors });
            }

            return Ok(authResult.User);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var createLoginCommand = new LoginUserCommand(loginDto);
            var authResult = await _mediator.Send(createLoginCommand);
            
            if (!authResult.Success)
            {
                return Unauthorized(new { Message = authResult.Message, Errors = authResult.Errors });
            }

            // Store user ID in session
            HttpContext.Session.SetInt32("UserId", authResult.User.Id);

            return Ok(authResult.User);
        }
    }

}

