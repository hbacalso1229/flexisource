using System.Net;
using System.Reflection;

namespace FitnessTracker.API.Common.ProducesResponseType
{
    public class NotFoundResponse : ServerResponseBase
    {
        public Warning Warning { get; set; }

        public NotFoundResponse()
        {
            Success = true;
            Warning = new Warning($"{Assembly.GetExecutingAssembly().GetName().Name}", $"{Assembly.GetExecutingAssembly().GetName().Version}", HttpStatusCode.NotFound.ToString(), StatusCodes.Status404NotFound, $"{Assembly.GetExecutingAssembly().GetName().Name}: {nameof(NotFoundResponse)}", "The {PropertyName} with the identifier 'xxxxx' was not found.") { };
        }
    }

}
