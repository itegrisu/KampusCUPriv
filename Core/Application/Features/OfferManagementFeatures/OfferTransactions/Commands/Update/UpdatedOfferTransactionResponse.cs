using Application.Features.Base;
using Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Update;

public class UpdatedOfferTransactionResponse : BaseResponse, IResponse
{
    public GetByGidOfferTransactionResponse Obj { get; set; }
}