using System.Net;
using System.Reflection;

namespace FitnessTracker.API.Common.ProducesResponseType
{
    public class UnprocessableEntityResponse : ServerResponseBase
    {
        public Warning Warning { get; set; }

        public UnprocessableEntityResponse()
        {
            dynamic details = new
            {
                PropertyName = "{PropertyName} is required.",
                PropertyValue = "{PropertyValue} is required."
            };
            Success = true;
            Warning = new Warning($"{Assembly.GetExecutingAssembly().GetName().Name}", $"{Assembly.GetExecutingAssembly().GetName().Version}", HttpStatusCode.UnprocessableEntity.ToString(), StatusCodes.Status422UnprocessableEntity, $"{Assembly.GetExecutingAssembly().GetName().Name}: {nameof(UnprocessableEntityResponse)}", "One or more validation errors occurred", details) { };
        }
    }
}
