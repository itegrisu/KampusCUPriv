namespace Application.Features.Base
{
    public class BaseQueryResponse<T> : BaseResponse
    {
        public T? Object { get; set; }
        public IEnumerable<T>? Objects { get; set; }
    }
}
