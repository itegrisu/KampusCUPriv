using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Rules;
using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Queries.GetByGid
{
    public class GetByGidGuestAccommodationResultQuery : IRequest<GetByGidGuestAccommodationResultResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidGuestAccommodationResultQueryHandler : IRequestHandler<GetByGidGuestAccommodationResultQuery, GetByGidGuestAccommodationResultResponse>
        {
            private readonly IMapper _mapper;
            private readonly IGuestAccommodationResultReadRepository _guestAccommodationResultReadRepository;
            private readonly GuestAccommodationResultBusinessRules _guestAccommodationResultBusinessRules;

            public GetByGidGuestAccommodationResultQueryHandler(IMapper mapper, IGuestAccommodationResultReadRepository guestAccommodationResultReadRepository, GuestAccommodationResultBusinessRules guestAccommodationResultBusinessRules)
            {
                _mapper = mapper;
                _guestAccommodationResultReadRepository = guestAccommodationResultReadRepository;
                _guestAccommodationResultBusinessRules = guestAccommodationResultBusinessRules;
            }

            public async Task<GetByGidGuestAccommodationResultResponse> Handle(GetByGidGuestAccommodationResultQuery request, CancellationToken cancellationToken)
            {
                X.GuestAccommodationResult? guestAccommodationResult = await _guestAccommodationResultReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.GuestAccommodationPersonFK).Include(x => x.GuestAccommodationRoomFK).ThenInclude(x => x.RoomTypeFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _guestAccommodationResultBusinessRules.GuestAccommodationResultShouldExistWhenSelected(guestAccommodationResult);

                GetByGidGuestAccommodationResultResponse response = _mapper.Map<GetByGidGuestAccommodationResultResponse>(guestAccommodationResult);
                return response;
            }
        }
    }
}