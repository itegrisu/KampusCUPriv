using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcern.Exceptions.WepApi.HttpProblemDetails
{
    public class InternalServerErrorProblemDetails : ProblemDetails
    {
        public InternalServerErrorProblemDetails(string detail)
        {
            base.Title = "Internal server error";
            base.Detail = detail;
            base.Status = 500;
            base.Type = "https://example.com/probs/internal";
        }
    }
}
