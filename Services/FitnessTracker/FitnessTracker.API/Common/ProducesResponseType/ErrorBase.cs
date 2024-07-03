namespace FitnessTracker.API.Common.ProducesResponseType
{
    public abstract class ErrorBase
    {
        public string Application { get; private set; }

        public string Version { get; private set; }

        public string Title { get; private set; }

        public int Status { get; private set; }

        public string Source { get; private set; }

        public string Message { get; private set; }

        public object Details { get; private set; }

        public ErrorBase(string application = "", string version = "", string title = "", int status = StatusCodes.Status500InternalServerError, string source = "", string message = "", object details = default)
        {
            Application = application;
            Version = version;
            Title = title;
            Status = status;
            Source = source;
            Message = message;
            Details = details ?? new { };
        }
    }
}
