using FitnessTracker.API.Controllers;
using FitnessTracker.Application.Commands.CreateUser;
using FitnessTracker.Application.Commands.CreateUserActivity;
using FitnessTracker.Application.Common.Models;
using FitnessTracker.Application.Queries;
using FitnessTracker.Tests.MockData;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace FitnessTracker.Tests.UnitTests.API
{
    public class UserControllerTests
    {
        private Mock<ILogger<UserController>> _logger = null!;

        public UserControllerTests()
        {
            _logger = new Mock<ILogger<UserController>>();
        }

        [Fact]
        public async Task GetUserActivity_ShouldReturnOkObjectResultAsync()
        {
            // Arrange
            UserActivityResponse technicianResponse = new UserData().GetData();

            Mock<IMediator> mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetUserActivityQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(technicianResponse);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(x => x.GetService(typeof(IMediator)))
                .Returns(mediator.Object);

            UserController controller = new UserController(_logger.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext { RequestServices = mockServiceProvider.Object },
                }
            };

            // Act
            var result = await controller.GetUserActivities(Guid.Empty, CancellationToken.None);

            // Assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result.Result);

            UserActivityResponse response = Assert.IsType<UserActivityResponse>(okObjectResult.Value);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnCreatedAtActionResultAsync()
        {
            // Arrange
            Guid userResponse = Guid.NewGuid();

            Mock<IMediator> mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userResponse);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(x => x.GetService(typeof(IMediator)))
                .Returns(mediator.Object);

            UserController controller = new UserController(_logger.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext { RequestServices = mockServiceProvider.Object },
                }
            };

            // Act
            var result = await controller.CreateUser(new CreateUserCommand 
            { 
                Name = "User Name", 
                Address =  "Makati City",
                BirthDate = DateTime.Now,
                Height = 8,
                Weight = 120
            }, CancellationToken.None);

            // Assert
            CreatedAtActionResult createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            Guid response = Assert.IsType<Guid>(createdAtActionResult.Value);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task AddUserActivities_ShouldReturnNoContentAsync()
        {
            // Arrange
            Guid userResponse = Guid.NewGuid();

            Mock<IMediator> mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<CreateUserActivityCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userResponse);

            Mock<IServiceProvider> mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider.Setup(x => x.GetService(typeof(IMediator)))
                .Returns(mediator.Object);

            UserController controller = new UserController(_logger.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext { RequestServices = mockServiceProvider.Object },
                }
            };

            // Act
            Guid userId = new Guid("6b7f0a08-0a78-41ad-bc00-4d02c02696b6");
            var result = await controller.AddUserActivity(userId, new CreateUserActivityCommand
            {
                UserId = userId,
                UserActivities = new List<CreateUserActivityDto>
                {
                    new CreateUserActivityDto
                    { 
                        Location = "Makati City",
                        DateStarted = DateTime.Now,
                        DateEnded = DateTime.Now,
                        Distance = 5
                    }
                }
            }, CancellationToken.None); ;

            // Assert
            NoContentResult response = Assert.IsType<NoContentResult>(result);

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
