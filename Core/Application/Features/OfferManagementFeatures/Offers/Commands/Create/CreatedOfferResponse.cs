using Application.Features.Base;
using Application.Features.OfferManagementFeatures.Offers.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OfferManagementFeatures.Offers.Commands.Create;

public class CreatedOfferResponse : BaseResponse, IResponse
{
    public GetByGidOfferResponse Obj { get; set; }
}