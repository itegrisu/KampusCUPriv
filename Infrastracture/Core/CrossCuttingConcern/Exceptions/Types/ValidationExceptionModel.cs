namespace Core.CrossCuttingConcern.Exceptions.Types
{
    public class ValidationExceptionModel
    {
        public string? Property { get; set; }

        public IEnumerable<string>? Errors { get; set; }
    }
}
