using Application.Features.OfferManagementFeatures.Offers.Rules;
using Application.Repositories.OfferManagementRepos.OfferRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.Offers.Queries.GetByGid
{
    public class GetByGidOfferQuery : IRequest<GetByGidOfferResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOfferQueryHandler : IRequestHandler<GetByGidOfferQuery, GetByGidOfferResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOfferReadRepository _offerReadRepository;
            private readonly OfferBusinessRules _offerBusinessRules;

            public GetByGidOfferQueryHandler(IMapper mapper, IOfferReadRepository offerReadRepository, OfferBusinessRules offerBusinessRules)
            {
                _mapper = mapper;
                _offerReadRepository = offerReadRepository;
                _offerBusinessRules = offerBusinessRules;
            }

            public async Task<GetByGidOfferResponse> Handle(GetByGidOfferQuery request, CancellationToken cancellationToken)
            {
                X.Offer? offer = await _offerReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _offerBusinessRules.OfferShouldExistWhenSelected(offer);

                GetByGidOfferResponse response = _mapper.Map<GetByGidOfferResponse>(offer);
                return response;
            }
        }
    }
}