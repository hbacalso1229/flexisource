namespace FitnessTracker.API.Common.ProducesResponseType
{
    public class Error : ErrorBase
    {
        public Error(string application = "", string version = "", string title = "", int status = StatusCodes.Status500InternalServerError, string source = "", string message = "", object details = default)
            : base(application, version, title, status, source, message, details)
        {
        }
    }
}
