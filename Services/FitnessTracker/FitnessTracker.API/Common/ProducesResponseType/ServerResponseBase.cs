namespace FitnessTracker.API.Common.ProducesResponseType
{
    public abstract class ServerResponseBase
    {
        public object Data { get; set; }

        public bool Success { get; set; }

        public DateTime DateTime { get; set; }

        public ServerResponseBase()
        {
            Data = new { };
            Success = false;
            DateTime = DateTime.Now;
        }
    }
}
