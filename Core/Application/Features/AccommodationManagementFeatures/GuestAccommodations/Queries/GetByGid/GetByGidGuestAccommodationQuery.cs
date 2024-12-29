using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.GuestAccommodations.Rules;
using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetByGid
{
    public class GetByGidGuestAccommodationQuery : IRequest<GetByGidGuestAccommodationResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidGuestAccommodationQueryHandler : IRequestHandler<GetByGidGuestAccommodationQuery, GetByGidGuestAccommodationResponse>
        {
            private readonly IMapper _mapper;
            private readonly IGuestAccommodationReadRepository _guestAccommodationReadRepository;
            private readonly GuestAccommodationBusinessRules _guestAccommodationBusinessRules;

            public GetByGidGuestAccommodationQueryHandler(IMapper mapper, IGuestAccommodationReadRepository guestAccommodationReadRepository, GuestAccommodationBusinessRules guestAccommodationBusinessRules)
            {
                _mapper = mapper;
                _guestAccommodationReadRepository = guestAccommodationReadRepository;
                _guestAccommodationBusinessRules = guestAccommodationBusinessRules;
            }

            public async Task<GetByGidGuestAccommodationResponse> Handle(GetByGidGuestAccommodationQuery request, CancellationToken cancellationToken)
            {
                X.GuestAccommodation? guestAccommodation = await _guestAccommodationReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.BuyCurrencyFK).Include(x => x.SellCurrencyFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _guestAccommodationBusinessRules.GuestAccommodationShouldExistWhenSelected(guestAccommodation);

                GetByGidGuestAccommodationResponse response = _mapper.Map<GetByGidGuestAccommodationResponse>(guestAccommodation);
                return response;
            }
        }
    }
}