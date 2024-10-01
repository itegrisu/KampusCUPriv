using Application.Features.Base;
using Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Create;

public class CreatedOfferTransactionResponse : BaseResponse, IResponse
{
    public GetByGidOfferTransactionResponse Obj { get; set; }
}