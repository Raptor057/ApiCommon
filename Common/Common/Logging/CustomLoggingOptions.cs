using Serilog.Events;

namespace Common.Logging
{
    public sealed class CustomLoggingOptions
    {
        public string Project { get; set; } = "UnknownProject";
        public string SeqUri { get; set; } = "";
        public LogEventLevel LogEventLevel { get; set; } = LogEventLevel.Information;
    }
}
