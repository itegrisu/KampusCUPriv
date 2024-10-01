using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.OfferManagementFeatures.Offers.Queries.GetByGid
{
    public class GetByGidOfferResponse : IResponse
    {
        public Guid Gid { get; set; }
        public string Title { get; set; }
        public string Customer { get; set; }
        public EnumOfferStatus OfferStatus { get; set; }

    }
}