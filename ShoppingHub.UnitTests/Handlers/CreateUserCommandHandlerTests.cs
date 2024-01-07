using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Users.Commands.CreateUser;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ShoppingHub.Domain.Entities;

namespace ShoppingHub.UnitTests.Handlers
{
    [TestFixture]
    public class CreateUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCommand_ReturnsAuthResultWithUser()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object);

            var command = new CreateUserCommand(new RegisterDto
            {
                UserName = "testuser",
                Password = "TestPassword123",
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789"
            });

            userRepositoryMock.Setup(x => x.IsUsernameTakenAsync(It.IsAny<string>())).ReturnsAsync(false);
            userRepositoryMock.Setup(x => x.IsEmailTakenAsync(It.IsAny<string>())).ReturnsAsync(false);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Success.Should().BeTrue();
            result.User.Should().NotBeNull();
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }


        [Test]
        public async Task Handle_InvalidCommand_ReturnsAuthResultWithErrors()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object);

            var command = new CreateUserCommand(new RegisterDto
            {
                UserName = "",  
                Password = "",  
                Email = "",     
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789"
            });

            var result = await handler.Handle(command, CancellationToken.None);

            result.Success.Should().BeFalse();
            result.User.Should().BeNull();
            result.Message.Should().Be("Invalid email or password");
            result.Errors.Should().NotBeNullOrEmpty();
        }
    }
}

