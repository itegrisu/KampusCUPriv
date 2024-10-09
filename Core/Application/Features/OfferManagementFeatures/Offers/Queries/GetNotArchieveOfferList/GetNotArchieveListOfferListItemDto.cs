using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.OfferManagementFeatures.Offers.Queries.GetNotArchieveOfferList
{
    public class GetNotArchieveListOfferListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public string Title { get; set; }
        public string Customer { get; set; }
        public EnumOfferStatus OfferStatus { get; set; }
    }
}
