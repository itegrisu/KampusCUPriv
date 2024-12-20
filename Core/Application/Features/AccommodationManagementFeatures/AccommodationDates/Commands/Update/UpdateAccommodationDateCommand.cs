using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Update;

public class UpdateAccommodationDateCommand : IRequest<UpdatedAccommodationDateResponse>
{
    public Guid Gid { get; set; }

	public Guid GidReservationDetailFK { get; set; }
public Guid GidGuestFK { get; set; }
public Guid GidRoomNoFK { get; set; }

public DateTime Date { get; set; }
public string? PreviousRoomInfo { get; set; }



    public class UpdateAccommodationDateCommandHandler : IRequestHandler<UpdateAccommodationDateCommand, UpdatedAccommodationDateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccommodationDateWriteRepository _accommodationDateWriteRepository;
        private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
        private readonly AccommodationDateBusinessRules _accommodationDateBusinessRules;

        public UpdateAccommodationDateCommandHandler(IMapper mapper, IAccommodationDateWriteRepository accommodationDateWriteRepository,
                                         AccommodationDateBusinessRules accommodationDateBusinessRules, IAccommodationDateReadRepository accommodationDateReadRepository)
        {
            _mapper = mapper;
            _accommodationDateWriteRepository = accommodationDateWriteRepository;
            _accommodationDateBusinessRules = accommodationDateBusinessRules;
            _accommodationDateReadRepository = accommodationDateReadRepository;
        }

        public async Task<UpdatedAccommodationDateResponse> Handle(UpdateAccommodationDateCommand request, CancellationToken cancellationToken)
        {
            X.AccommodationDate? accommodationDate = await _accommodationDateReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.GuestFK).Include(x => x.ReservationDetailFK).Include(x => x.ReservationRoomFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _accommodationDateBusinessRules.AccommodationDateShouldExistWhenSelected(accommodationDate);
            accommodationDate = _mapper.Map(request, accommodationDate);

            _accommodationDateWriteRepository.Update(accommodationDate!);
            await _accommodationDateWriteRepository.SaveAsync();
            GetByGidAccommodationDateResponse obj = _mapper.Map<GetByGidAccommodationDateResponse>(accommodationDate);

            return new()
            {
                Title = AccommodationDatesBusinessMessages.ProcessCompleted,
                Message = AccommodationDatesBusinessMessages.SuccessCreatedAccommodationDateMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}