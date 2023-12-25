using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShoppingHub.Application.Abstractions.Services;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Validations.DTO;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Infrastructure.Repositories;
using ShoppingHub.Infrastructure.Services;

namespace ShoppingHub.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var authResult = await _authService.LoginAsync(loginDto);

            // If authentication failed
            if (!authResult.Success)
            {
                return Unauthorized(new { Message = authResult.Message, Errors = authResult.Errors });
            }

            return Ok(authResult.User);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var authResult = await _authService.RegisterAsync(registerDto);

            // If registration failed
            if (!authResult.Success)
            {
                return BadRequest(new { Message = authResult.Message, Errors = authResult.Errors });
            }

            return Ok(authResult.User);
        }
    }

}

