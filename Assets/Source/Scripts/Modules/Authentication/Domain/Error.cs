namespace EnvilopeChako.Modules.Authentication.Domain
{
    public enum LogLevel { Info, Warn, Error }

    public record Error
    {
        public string Message { get; init; }
        public LogLevel Level { get; init; }
        public bool UserFacing { get; init; }

        public Error(string message)
        {
            Message = message;
            Level = LogLevel.Error;
            UserFacing = true;
        }

        public Error(string message, LogLevel level, bool userFacing)
        {
            Message = message;
            Level = level;
            UserFacing = userFacing;
        }
    }
}