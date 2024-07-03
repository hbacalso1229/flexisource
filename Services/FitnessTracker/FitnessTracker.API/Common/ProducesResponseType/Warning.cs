namespace FitnessTracker.API.Common.ProducesResponseType
{
    public class Warning : ErrorBase
    {
        public Warning(string application = "", string version = "", string title = "", int status = StatusCodes.Status404NotFound, string source = "", string message = "", object details = default)
            : base(application, version, title, status, source, message, details)
        {
        }
    }
}
