using Application.Features.OfferManagementFeatures.OfferTransactions.Constants;
using Application.Features.OfferManagementFeatures.OfferTransactions.Rules;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using AutoMapper;
using X = Domain.Entities.OfferManagements;
using MediatR;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Delete;

public class DeleteOfferTransactionCommand : IRequest<DeletedOfferTransactionResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOfferTransactionCommandHandler : IRequestHandler<DeleteOfferTransactionCommand, DeletedOfferTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferTransactionReadRepository _offerTransactionReadRepository;
        private readonly IOfferTransactionWriteRepository _offerTransactionWriteRepository;
        private readonly OfferTransactionBusinessRules _offerTransactionBusinessRules;

        public DeleteOfferTransactionCommandHandler(IMapper mapper, IOfferTransactionReadRepository offerTransactionReadRepository,
                                         OfferTransactionBusinessRules offerTransactionBusinessRules, IOfferTransactionWriteRepository offerTransactionWriteRepository)
        {
            _mapper = mapper;
            _offerTransactionReadRepository = offerTransactionReadRepository;
            _offerTransactionBusinessRules = offerTransactionBusinessRules;
            _offerTransactionWriteRepository = offerTransactionWriteRepository;
        }

        public async Task<DeletedOfferTransactionResponse> Handle(DeleteOfferTransactionCommand request, CancellationToken cancellationToken)
        {
            X.OfferTransaction? offerTransaction = await _offerTransactionReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _offerTransactionBusinessRules.OfferTransactionShouldExistWhenSelected(offerTransaction);
            offerTransaction.DataState = Core.Enum.DataState.Deleted;

            _offerTransactionWriteRepository.Update(offerTransaction);
            await _offerTransactionWriteRepository.SaveAsync();

            return new()
            {
                Title = OfferTransactionsBusinessMessages.ProcessCompleted,
                Message = OfferTransactionsBusinessMessages.SuccessDeletedOfferTransactionMessage,
                IsValid = true
            };
        }
    }
}