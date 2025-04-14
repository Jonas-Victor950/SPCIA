namespace BRCSystem.ClassLibrary.Logs
{
    public class Log
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public DateTime RequestTime { get; set; }
        public string ClientIp { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string ResponseStatus { get; set; }
        public string? RequestBody { get; set; }
    }
}
