namespace Core.Application.Request
{
    public class PageRequest
    {
        public int PageIndex { get; set; } = 0; 
        public int PageSize { get; set; } = 10;
    }
}
