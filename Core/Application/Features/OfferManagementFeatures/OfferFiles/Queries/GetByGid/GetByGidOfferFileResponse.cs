using Core.Application.Responses;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByGid
{
    public class GetByGidOfferFileResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidOfferFK { get; set; }
        public string OfferFKTitle { get; set; }

        public string Title { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }

    }
}