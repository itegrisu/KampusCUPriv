using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Create;

public class CreateAccommodationDateCommand : IRequest<CreatedAccommodationDateResponse>
{
    public Guid GidReservationDetailFK { get; set; }
    public Guid GidGuestFK { get; set; }
    public Guid GidRoomNoFK { get; set; }

    public DateTime[] Date { get; set; }
    public string? PreviousRoomInfo { get; set; }



    public class CreateAccommodationDateCommandHandler : IRequestHandler<CreateAccommodationDateCommand, CreatedAccommodationDateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccommodationDateWriteRepository _accommodationDateWriteRepository;
        private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
        private readonly AccommodationDateBusinessRules _accommodationDateBusinessRules;

        public CreateAccommodationDateCommandHandler(IMapper mapper, IAccommodationDateWriteRepository accommodationDateWriteRepository,
                                         AccommodationDateBusinessRules accommodationDateBusinessRules, IAccommodationDateReadRepository accommodationDateReadRepository)
        {
            _mapper = mapper;
            _accommodationDateWriteRepository = accommodationDateWriteRepository;
            _accommodationDateBusinessRules = accommodationDateBusinessRules;
            _accommodationDateReadRepository = accommodationDateReadRepository;
        }

        public async Task<CreatedAccommodationDateResponse> Handle(CreateAccommodationDateCommand request, CancellationToken cancellationToken)
        {
            await _accommodationDateBusinessRules.IsReservationDetailFKExist(request.GidReservationDetailFK);
            await _accommodationDateBusinessRules.IsGuestFKExist(request.GidGuestFK);
            await _accommodationDateBusinessRules.IsRoomNoFKExist(request.GidRoomNoFK);

            for (int i = 0; i < request.Date.Length; i++)
            {
                X.AccommodationDate accommodationDate = new()
                {
                    Date = request.Date[i],
                    GidGuestFK = request.GidGuestFK,
                    GidReservationDetailFK = request.GidReservationDetailFK,
                    GidRoomNoFK = request.GidRoomNoFK,
                    PreviousRoomInfo = request.PreviousRoomInfo,
                };
                await _accommodationDateWriteRepository.AddAsync(accommodationDate);
                await _accommodationDateWriteRepository.SaveAsync();
            }

            //X.AccommodationDate savedAccommodationDate = await _accommodationDateReadRepository.GetAsync(predicate: x => x.Gid == accommodationDate.Gid,
            //    include: x => x.Include(x => x.GuestFK).Include(x => x.ReservationDetailFK).Include(x => x.ReservationRoomFK));

            //GetByGidAccommodationDateResponse obj = _mapper.Map<GetByGidAccommodationDateResponse>(savedAccommodationDate);
            return new()
            {
                Title = AccommodationDatesBusinessMessages.ProcessCompleted,
                Message = AccommodationDatesBusinessMessages.SuccessCreatedAccommodationDateMessage,
                IsValid = true,
                //Obj = obj
            };
        }
    }
}