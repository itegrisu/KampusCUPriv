using Application.Features.Base;
using Application.Features.OfferManagementFeatures.Offers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OfferManagementFeatures.Offers.Commands.Update;

public class UpdatedOfferResponse : BaseResponse, IResponse
{
    public GetByGidOfferResponse Obj { get; set; }
}