using Application.Features.OfferManagementFeatures.Offers.Constants;
using Application.Features.OfferManagementFeatures.Offers.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.Offers.Rules;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.Offers.Commands.Update;

public class UpdateOfferCommand : IRequest<UpdatedOfferResponse>
{
    public Guid Gid { get; set; }

    public string Title { get; set; }
    public string Customer { get; set; }
    public EnumOfferStatus OfferStatus { get; set; }



    public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, UpdatedOfferResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferWriteRepository _offerWriteRepository;
        private readonly IOfferReadRepository _offerReadRepository;
        private readonly OfferBusinessRules _offerBusinessRules;

        public UpdateOfferCommandHandler(IMapper mapper, IOfferWriteRepository offerWriteRepository,
                                         OfferBusinessRules offerBusinessRules, IOfferReadRepository offerReadRepository)
        {
            _mapper = mapper;
            _offerWriteRepository = offerWriteRepository;
            _offerBusinessRules = offerBusinessRules;
            _offerReadRepository = offerReadRepository;
        }

        public async Task<UpdatedOfferResponse> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            X.Offer? offer = await _offerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);

            await _offerBusinessRules.OfferShouldExistWhenSelected(offer);
            offer = _mapper.Map(request, offer);

            _offerWriteRepository.Update(offer!);
            await _offerWriteRepository.SaveAsync();
            GetByGidOfferResponse obj = _mapper.Map<GetByGidOfferResponse>(offer);

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