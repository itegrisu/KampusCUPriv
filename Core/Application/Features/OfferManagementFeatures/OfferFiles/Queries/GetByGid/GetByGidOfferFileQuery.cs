using Application.Features.OfferManagementFeatures.OfferFiles.Rules;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByGid
{
    public class GetByGidOfferFileQuery : IRequest<GetByGidOfferFileResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidOfferFileQueryHandler : IRequestHandler<GetByGidOfferFileQuery, GetByGidOfferFileResponse>
        {
            private readonly IMapper _mapper;
            private readonly IOfferFileReadRepository _offerFileReadRepository;
            private readonly OfferFileBusinessRules _offerFileBusinessRules;

            public GetByGidOfferFileQueryHandler(IMapper mapper, IOfferFileReadRepository offerFileReadRepository, OfferFileBusinessRules offerFileBusinessRules)
            {
                _mapper = mapper;
                _offerFileReadRepository = offerFileReadRepository;
                _offerFileBusinessRules = offerFileBusinessRules;
            }

            public async Task<GetByGidOfferFileResponse> Handle(GetByGidOfferFileQuery request, CancellationToken cancellationToken)
            {
                X.OfferFile? offerFile = await _offerFileReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.OfferFK));
                //unutma
                //includes varsa eklenecek - Orn: Altta
                //include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _offerFileBusinessRules.OfferFileShouldExistWhenSelected(offerFile);

                GetByGidOfferFileResponse response = _mapper.Map<GetByGidOfferFileResponse>(offerFile);
                return response;
            }
        }
    }
}