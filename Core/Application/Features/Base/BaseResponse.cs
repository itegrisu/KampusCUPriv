namespace Application.Features.Base
{
    public class BaseResponse
    {
        public string? Title { get; set; }
        public string? ActionType { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsValid { get; set; }
    }
}
