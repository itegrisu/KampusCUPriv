namespace Core.CrossCuttingConcern.Exceptions.Types
{
    public class ValidationException : System.Exception
    {
        public IEnumerable<ValidationExceptionModel> Errors { get; }

        public ValidationException()
        {
            Errors = Array.Empty<ValidationExceptionModel>();
        }

        public ValidationException(string? message)
            : base(message)
        {
            Errors = Array.Empty<ValidationExceptionModel>();
        }

        public ValidationException(string? message, System.Exception? innerException)
            : base(message, innerException)
        {
            Errors = Array.Empty<ValidationExceptionModel>();
        }

        public ValidationException(IEnumerable<ValidationExceptionModel> errors)
            : base(BuildErrorMessage(errors))
        {
            Errors = errors;
        }

        private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
        {
            IEnumerable<string> values = errors.Select((ValidationExceptionModel x) => $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, x.Errors ?? Array.Empty<string>())}");
            return "Validation failed: " + string.Join(string.Empty, values);
        }
    }
}
