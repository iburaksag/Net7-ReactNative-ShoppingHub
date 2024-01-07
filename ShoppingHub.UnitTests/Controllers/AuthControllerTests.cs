using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Users.Commands.CreateUser;
using ShoppingHub.Application.Users.Commands.LoginUser;
using ShoppingHub.Domain;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Presentation.Controllers;
using ShoppingHub.UnitTests.Shared;

namespace ShoppingHub.UnitTests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        //REGISTER
        [Test]
        public async Task Register_ValidRegistration_ReturnsOkResult()
        {
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<AuthController>>();

            var controller = new AuthController(mediatorMock.Object, loggerMock.Object);

            var registerDto = new RegisterDto
            {
                UserName = "testuser",
                Password = "TestPassword123",
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789"
            };

            var authResult = new AuthResult { Success = true, User = new User { UserName = "testuser" } };
            mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), default)).ReturnsAsync(authResult);

            var result = await controller.Register(registerDto);

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeSameAs(authResult.User);
        }

        [Test]
        public async Task Register_InvalidRegistration_ReturnsBadRequest()
        {
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<AuthController>>();

            var controller = new AuthController(mediatorMock.Object, loggerMock.Object);

            var registerDto = new RegisterDto(); 

            var authResult = new AuthResult { Success = false, Message = "Invalid email or password" };
            mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), default)).ReturnsAsync(authResult);

            var result = await controller.Register(registerDto);

            result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result).Value.Should().BeEquivalentTo(new { Message = authResult.Message });
        }

        [Test]
        public async Task Login_InvalidLogin_ReturnsUnauthorizedResult()
        {
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<AuthController>>();

            var controller = new AuthController(mediatorMock.Object, loggerMock.Object);

            var loginDto = new LoginDto("test", "");


            var authResult = new AuthResult { Success = false, Message = "Invalid email or password" };
            mediatorMock.Setup(x => x.Send(It.IsAny<LoginUserCommand>(), default)).ReturnsAsync(authResult);

            var result = await controller.Login(loginDto);

            result.Should().BeOfType<UnauthorizedObjectResult>();
            ((UnauthorizedObjectResult)result).Value.Should().BeEquivalentTo(new { Message = authResult.Message });
        }

    }
}

