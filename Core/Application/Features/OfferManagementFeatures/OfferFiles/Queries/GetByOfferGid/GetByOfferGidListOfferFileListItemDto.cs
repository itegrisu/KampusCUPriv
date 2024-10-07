using Core.Application.Dtos;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByOfferGid
{
    public class GetByOfferGidListOfferFileListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidOfferFK { get; set; }
        public string OfferFKTitle { get; set; }
        public string Title { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }

    }
}
