using Application.Features.OfferManagementFeatures.OfferTransactions.Rules;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByGid
{
    public class GetByGidOfferTransactionQuery : IRequest<GetByGidOfferTransactionResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOfferTransactionQueryHandler : IRequestHandler<GetByGidOfferTransactionQuery, GetByGidOfferTransactionResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOfferTransactionReadRepository _offerTransactionReadRepository;
            private readonly OfferTransactionBusinessRules _offerTransactionBusinessRules;

            public GetByGidOfferTransactionQueryHandler(IMapper mapper, IOfferTransactionReadRepository offerTransactionReadRepository, OfferTransactionBusinessRules offerTransactionBusinessRules)
            {
                _mapper = mapper;
                _offerTransactionReadRepository = offerTransactionReadRepository;
                _offerTransactionBusinessRules = offerTransactionBusinessRules;
            }

            public async Task<GetByGidOfferTransactionResponse> Handle(GetByGidOfferTransactionQuery request, CancellationToken cancellationToken)
            {
                X.OfferTransaction? offerTransaction = await _offerTransactionReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.CurrencyFK).Include(x => x.OfferFK));

                await _offerTransactionBusinessRules.OfferTransactionShouldExistWhenSelected(offerTransaction);

                GetByGidOfferTransactionResponse response = _mapper.Map<GetByGidOfferTransactionResponse>(offerTransaction);
                return response;
            }
        }
    }
}