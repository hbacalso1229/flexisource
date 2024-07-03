using FitnessTracker.API.SwaggerExamples.Responses;
using FitnessTracker.Application.Commands.CreateUser;
using FitnessTracker.Application.Commands.CreateUserActivity;
using FitnessTracker.Application.Common.Models;
using FitnessTracker.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers
{
    [ApiController]
    [Route("v{version:ApiVersion}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiVersion("1.0")]
    public class UserController : ApiControllerBase
    {
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicianController"/> class.
        /// </summary>
        /// <param name="logger"></param>
        public UserController(ILogger<UserController> logger)
        {
            ArgumentNullException.ThrowIfNull(logger);

            _logger = logger;
            _logger.LogInformation($"Initializing {nameof(UserController)}...");
        }

        /// <summary>
        /// Creates new user profile
        /// </summary>
        /// <param name="command">A user profile request</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>Newly created user profile</returns>
        [HttpPost("users")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created, Type = typeof(CreateUserResponseExample))]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            Guid response = await Mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(CreateUser), new { response }, response);
        }

        /// <summary>
        /// Add user activity
        /// </summary>
        /// <param name="userId">A user id</param>
        /// <param name="command">A user activity request</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>Added user activity</returns>
        [HttpPost("users/{userId}/activities")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Guid>> AddUserActivity(Guid userId, [FromBody] CreateUserActivityCommand command, CancellationToken cancellationToken)
        {
            if (userId != command.UserId)
            {
                return BadRequest($"The user id '{userId.ToString()}' route path value does not match the request payload '{command.UserId}'.");
            }

            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Add user activity
        /// </summary>
        /// <param name="userId">A user id</param>
        /// <param name="command">A user activity request</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>Added user activity</returns>
        [HttpGet("users/{userId}/activities")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UserActivityResponse), StatusCodes.Status200OK, Type = typeof(UserActivityResponseExample))]
        public async Task<ActionResult<UserActivityResponse>> GetUserActivities(Guid userId, CancellationToken cancellationToken)
        {
            UserActivityResponse response = await Mediator.Send(new GetUserActivityQuery(userId), cancellationToken);

            return Ok(response);
        }
    }
}
