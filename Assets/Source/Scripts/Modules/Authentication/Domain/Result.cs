namespace EnvilopeChako.Modules.Authentication.Domain
{
    public record Result
    {
        public Error Error { get; init; }
        public int Code { get; init; }
        public string Context { get; init; }

        public bool IsSuccess => Error == null;

        public static Result Ok => new Result { Error = null, Code = 0, Context = string.Empty };

        public static Result Fail(string message, int code = 1, string context = "")
        {
            return new Result
            {
                Error = new Error(message),
                Code = code,
                Context = context
            };
        }
    }
}