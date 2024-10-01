using Application.Features.OfferManagementFeatures.Offers.Constants;
using Application.Features.OfferManagementFeatures.Offers.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.Offers.Rules;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.Offers.Commands.Create;

public class CreateOfferCommand : IRequest<CreatedOfferResponse>
{

    public string Title { get; set; }
    public string Customer { get; set; }
    public EnumOfferStatus OfferStatus { get; set; }



    public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, CreatedOfferResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferWriteRepository _offerWriteRepository;
        private readonly IOfferReadRepository _offerReadRepository;
        private readonly OfferBusinessRules _offerBusinessRules;

        public CreateOfferCommandHandler(IMapper mapper, IOfferWriteRepository offerWriteRepository,
                                         OfferBusinessRules offerBusinessRules, IOfferReadRepository offerReadRepository)
        {
            _mapper = mapper;
            _offerWriteRepository = offerWriteRepository;
            _offerBusinessRules = offerBusinessRules;
            _offerReadRepository = offerReadRepository;
        }

        public async Task<CreatedOfferResponse> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {

            X.Offer offer = _mapper.Map<X.Offer>(request);

            await _offerWriteRepository.AddAsync(offer);
            await _offerWriteRepository.SaveAsync();

            X.Offer savedOffer = await _offerReadRepository.GetAsync(predicate: x => x.Gid == offer.Gid);


            GetByGidOfferResponse obj = _mapper.Map<GetByGidOfferResponse>(savedOffer);
            return new()
            {
                Title = OffersBusinessMessages.ProcessCompleted,
                Message = OffersBusinessMessages.SuccessCreatedOfferMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}