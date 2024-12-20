using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;
using Microsoft.EntityFrameworkCore;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetByGid
{
    public class GetByGidAccommodationDateQuery : IRequest<GetByGidAccommodationDateResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidAccommodationDateQueryHandler : IRequestHandler<GetByGidAccommodationDateQuery, GetByGidAccommodationDateResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
            private readonly AccommodationDateBusinessRules _accommodationDateBusinessRules;

            public GetByGidAccommodationDateQueryHandler(IMapper mapper, IAccommodationDateReadRepository accommodationDateReadRepository, AccommodationDateBusinessRules accommodationDateBusinessRules)
            {
                _mapper = mapper;
                _accommodationDateReadRepository = accommodationDateReadRepository;
                _accommodationDateBusinessRules = accommodationDateBusinessRules;
            }

            public async Task<GetByGidAccommodationDateResponse> Handle(GetByGidAccommodationDateQuery request, CancellationToken cancellationToken)
            {
                X.AccommodationDate? accommodationDate = await _accommodationDateReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.GuestFK).Include(x => x.ReservationDetailFK).Include(x => x.ReservationRoomFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _accommodationDateBusinessRules.AccommodationDateShouldExistWhenSelected(accommodationDate);

                GetByGidAccommodationDateResponse response = _mapper.Map<GetByGidAccommodationDateResponse>(accommodationDate);
                return response;
            }
        }
    }
}