using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Users.Commands.LoginUser;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.UnitTests.Shared;

namespace ShoppingHub.UnitTests.Handlers
{
    [TestFixture]
    public class LoginUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsAuthResultWithUser()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var handler = new LoginUserCommandHandler(userRepositoryMock.Object);

            var command = new LoginUserCommand(new LoginDto("buraks@gmail.com", "123456"));

            byte[] passwordHash, passwordSalt;
            CreatePassword.CreatePasswordHash("123456", out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = "buraks@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Success.Should().BeTrue();
            result.User.Should().NotBeNull();
            result.User.Should().BeSameAs(user);
        }

        [Test]
        public async Task Handle_InvalidCommand_ReturnsAuthResultWithFailure()
        {
            
            var userRepositoryMock = new Mock<IUserRepository>();
            var handler = new LoginUserCommandHandler(userRepositoryMock.Object);

            var command = new LoginUserCommand(new LoginDto("", ""));

            var result = await handler.Handle(command, CancellationToken.None);
            
            result.Success.Should().BeFalse();
            result.User.Should().BeNull();
            result.Message.Should().Be("Validation failed");
            result.Errors.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Handle_UserNotFound_ReturnsAuthResultWithFailure()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var handler = new LoginUserCommandHandler(userRepositoryMock.Object);

            var command = new LoginUserCommand(new LoginDto("nonexistent@example.com", "SomePassword"));

            userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Success.Should().BeFalse();
            result.User.Should().BeNull();
            result.Message.Should().Be("Invalid email or password");
        }

    }

}

