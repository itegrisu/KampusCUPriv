using Application.Features.Base;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.Create;

public class CreatedOfferFileResponse : BaseResponse, IResponse
{
    public GetByGidOfferFileResponse Obj { get; set; }
}