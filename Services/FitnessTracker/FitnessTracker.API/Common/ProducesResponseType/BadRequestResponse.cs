using FitnessTracker.API.Common.Base;
using System.Reflection;

namespace FitnessTracker.API.Common.ProducesResponseType
{
    public class BadRequestResponse : ServiceResponseBase
    {
        public BadRequestResponse()
        {
            Data = new { };
            Success = true;
            ResponseInfo = new ResponseInfo(ResponseType.Warning, StatusCodes.Status400BadRequest.ToString(), $"{Assembly.GetExecutingAssembly().GetName().Name}: {nameof(BadRequestResponse)}", $"The client request query 'test' is not matched from the request body 'testx'");
        }
    }
}
