using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Rules;
using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Queries.GetByGid
{
    public class GetByGidGuestAccommodationPersonQuery : IRequest<GetByGidGuestAccommodationPersonResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidGuestAccommodationPersonQueryHandler : IRequestHandler<GetByGidGuestAccommodationPersonQuery, GetByGidGuestAccommodationPersonResponse>
        {
            private readonly IMapper _mapper;
            private readonly IGuestAccommodationPersonReadRepository _guestAccommodationPersonReadRepository;
            private readonly GuestAccommodationPersonBusinessRules _guestAccommodationPersonBusinessRules;

            public GetByGidGuestAccommodationPersonQueryHandler(IMapper mapper, IGuestAccommodationPersonReadRepository guestAccommodationPersonReadRepository, GuestAccommodationPersonBusinessRules guestAccommodationPersonBusinessRules)
            {
                _mapper = mapper;
                _guestAccommodationPersonReadRepository = guestAccommodationPersonReadRepository;
                _guestAccommodationPersonBusinessRules = guestAccommodationPersonBusinessRules;
            }

            public async Task<GetByGidGuestAccommodationPersonResponse> Handle(GetByGidGuestAccommodationPersonQuery request, CancellationToken cancellationToken)
            {
                X.GuestAccommodationPerson? guestAccommodationPerson = await _guestAccommodationPersonReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.GuestAccommodationFK).Include(x => x.CountryFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _guestAccommodationPersonBusinessRules.GuestAccommodationPersonShouldExistWhenSelected(guestAccommodationPerson);

                GetByGidGuestAccommodationPersonResponse response = _mapper.Map<GetByGidGuestAccommodationPersonResponse>(guestAccommodationPerson);
                return response;
            }
        }
    }
}