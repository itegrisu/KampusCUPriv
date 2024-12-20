using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.Guests.Rules;
using Application.Repositories.AccommodationManagements.GuestRepo;

namespace Application.Features.AccommodationManagementFeatures.Guests.Queries.GetByGid
{
    public class GetByGidGuestQuery : IRequest<GetByGidGuestResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidGuestQueryHandler : IRequestHandler<GetByGidGuestQuery, GetByGidGuestResponse>
        {
            private readonly IMapper _mapper;
            private readonly IGuestReadRepository _guestReadRepository;
            private readonly GuestBusinessRules _guestBusinessRules;

            public GetByGidGuestQueryHandler(IMapper mapper, IGuestReadRepository guestReadRepository, GuestBusinessRules guestBusinessRules)
            {
                _mapper = mapper;
                _guestReadRepository = guestReadRepository;
                _guestBusinessRules = guestBusinessRules;
            }

            public async Task<GetByGidGuestResponse> Handle(GetByGidGuestQuery request, CancellationToken cancellationToken)
            {
                X.Guest? guest = await _guestReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.CountryFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _guestBusinessRules.GuestShouldExistWhenSelected(guest);

                GetByGidGuestResponse response = _mapper.Map<GetByGidGuestResponse>(guest);
                return response;
            }
        }
    }
}