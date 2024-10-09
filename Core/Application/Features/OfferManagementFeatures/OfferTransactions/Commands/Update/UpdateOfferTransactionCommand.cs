using Application.Features.OfferManagementFeatures.OfferTransactions.Constants;
using Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.OfferTransactions.Rules;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Update;

public class UpdateOfferTransactionCommand : IRequest<UpdatedOfferTransactionResponse>
{
    public Guid Gid { get; set; }

    public Guid GidOfferFK { get; set; }
    public Guid GidCurrencyFK { get; set; }
    public decimal Total { get; set; }
    public DateTime? OfferDeadline { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class UpdateOfferTransactionCommandHandler : IRequestHandler<UpdateOfferTransactionCommand, UpdatedOfferTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferTransactionWriteRepository _offerTransactionWriteRepository;
        private readonly IOfferTransactionReadRepository _offerTransactionReadRepository;
        private readonly OfferTransactionBusinessRules _offerTransactionBusinessRules;

        public UpdateOfferTransactionCommandHandler(IMapper mapper, IOfferTransactionWriteRepository offerTransactionWriteRepository,
                                         OfferTransactionBusinessRules offerTransactionBusinessRules, IOfferTransactionReadRepository offerTransactionReadRepository)
        {
            _mapper = mapper;
            _offerTransactionWriteRepository = offerTransactionWriteRepository;
            _offerTransactionBusinessRules = offerTransactionBusinessRules;
            _offerTransactionReadRepository = offerTransactionReadRepository;
        }

        public async Task<UpdatedOfferTransactionResponse> Handle(UpdateOfferTransactionCommand request, CancellationToken cancellationToken)
        {
            X.OfferTransaction? offerTransaction = await _offerTransactionReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _offerTransactionBusinessRules.OfferTransactionShouldExistWhenSelected(offerTransaction);
            await _offerTransactionBusinessRules.OfferShouldExistWhenSelected(request.GidOfferFK);
            await _offerTransactionBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);
            offerTransaction = _mapper.Map(request, offerTransaction);

            _offerTransactionWriteRepository.Update(offerTransaction!);
            await _offerTransactionWriteRepository.SaveAsync();

            X.OfferTransaction updatedOfferTransaction = await _offerTransactionReadRepository.GetAsync(predicate: x => x.Gid == offerTransaction.Gid, include: x => x.Include(x => x.CurrencyFK).Include(x => x.OfferFK));

            GetByGidOfferTransactionResponse obj = _mapper.Map<GetByGidOfferTransactionResponse>(updatedOfferTransaction);

            return new()
            {
                Title = OfferTransactionsBusinessMessages.ProcessCompleted,
                Message = OfferTransactionsBusinessMessages.SuccessUpdatedOfferTransactionMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}