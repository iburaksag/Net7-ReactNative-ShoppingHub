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
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var createUserCommand = new CreateUserCommand(registerDto);
                var authResult = await _mediator.Send(createUserCommand);

                if (!authResult.Success)
                {
                    _logger.LogError("Registration failed.");
                    return BadRequest(new { Message = authResult.Message, Errors = authResult.Errors });
                }

                _logger.LogInformation("User {Email} registered successfully", registerDto.Email);
                return Ok(authResult.User);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during register");
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var createLoginCommand = new LoginUserCommand(loginDto);
                var authResult = await _mediator.Send(createLoginCommand);

                if (!authResult.Success)
                {
                    _logger.LogError("Login failed for user {Email}", loginDto.Email);
                    return Unauthorized(new { Message = authResult.Message, Errors = authResult.Errors });
                }

                // Store user ID in session
                HttpContext.Session.SetInt32("UserId", authResult.User.Id);

                _logger.LogInformation("User {Email} logged in successfully", loginDto.Email);
                return Ok(authResult.User);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login");
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }
    }

}

