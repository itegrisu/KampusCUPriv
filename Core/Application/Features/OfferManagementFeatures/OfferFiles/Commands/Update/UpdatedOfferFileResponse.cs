using Application.Features.Base;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.Update;

public class UpdatedOfferFileResponse : BaseResponse, IResponse
{
    public GetByGidOfferFileResponse Obj { get; set; }
}