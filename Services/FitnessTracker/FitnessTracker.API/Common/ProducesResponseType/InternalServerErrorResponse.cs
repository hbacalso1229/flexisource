using System.Net;
using System.Reflection;

namespace FitnessTracker.API.Common.ProducesResponseType
{
    public class InternalServerErrorResponse : ServerResponseBase
    {
        public Error Error { get; set; }

        public InternalServerErrorResponse()
        {
            Error = new Error($"{Assembly.GetExecutingAssembly().GetName().Name}", $"{Assembly.GetExecutingAssembly().GetName().Version}", HttpStatusCode.InternalServerError.ToString(), StatusCodes.Status500InternalServerError, $"{Assembly.GetExecutingAssembly().GetName().Name}: {nameof(InternalServerErrorResponse)}", "Value cannot be null. (Parameter: {PropertyName})") { };
        }
    }
}
