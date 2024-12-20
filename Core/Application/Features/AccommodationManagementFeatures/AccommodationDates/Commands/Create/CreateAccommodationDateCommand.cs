using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetByGid;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Create;

public class CreateAccommodationDateCommand : IRequest<CreatedAccommodationDateResponse>
{
    public Guid GidReservationDetailFK { get; set; }
public Guid GidGuestFK { get; set; }
public Guid GidRoomNoFK { get; set; }

public DateTime Date { get; set; }
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
            //int maxRowNo = await _accommodationDateReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.AccommodationDate accommodationDate = _mapper.Map<X.AccommodationDate>(request);
            //accommodationDate.RowNo = maxRowNo + 1;

            await _accommodationDateWriteRepository.AddAsync(accommodationDate);
            await _accommodationDateWriteRepository.SaveAsync();

			X.AccommodationDate savedAccommodationDate = await _accommodationDateReadRepository.GetAsync(predicate: x => x.Gid == accommodationDate.Gid,
                include: x => x.Include(x => x.GuestFK).Include(x => x.ReservationDetailFK).Include(x => x.ReservationRoomFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidAccommodationDateResponse obj = _mapper.Map<GetByGidAccommodationDateResponse>(savedAccommodationDate);
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