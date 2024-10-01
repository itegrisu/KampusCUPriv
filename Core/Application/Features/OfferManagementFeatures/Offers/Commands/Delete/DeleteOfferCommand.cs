using Application.Features.OfferManagementFeatures.Offers.Constants;
using Application.Features.OfferManagementFeatures.Offers.Rules;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using AutoMapper;
using X = Domain.Entities.OfferManagements;
using MediatR;

namespace Application.Features.OfferManagementFeatures.Offers.Commands.Delete;

public class DeleteOfferCommand : IRequest<DeletedOfferResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, DeletedOfferResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferReadRepository _offerReadRepository;
        private readonly IOfferWriteRepository _offerWriteRepository;
        private readonly OfferBusinessRules _offerBusinessRules;

        public DeleteOfferCommandHandler(IMapper mapper, IOfferReadRepository offerReadRepository,
                                         OfferBusinessRules offerBusinessRules, IOfferWriteRepository offerWriteRepository)
        {
            _mapper = mapper;
            _offerReadRepository = offerReadRepository;
            _offerBusinessRules = offerBusinessRules;
            _offerWriteRepository = offerWriteRepository;
        }

        public async Task<DeletedOfferResponse> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            X.Offer? offer = await _offerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _offerBusinessRules.OfferShouldExistWhenSelected(offer);
            offer.DataState = Core.Enum.DataState.Deleted;

            _offerWriteRepository.Update(offer);
            await _offerWriteRepository.SaveAsync();

            return new()
            {
                Title = OffersBusinessMessages.ProcessCompleted,
                Message = OffersBusinessMessages.SuccessDeletedOfferMessage,
                IsValid = true
            };
        }
    }
}