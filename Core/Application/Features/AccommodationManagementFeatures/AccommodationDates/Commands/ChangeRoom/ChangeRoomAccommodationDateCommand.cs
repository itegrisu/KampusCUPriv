using Application.Abstractions.UnitOfWork;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using AutoMapper;
using Core.CrossCuttingConcern.Exceptions;
using Core.Enum;
using Domain.Entities.AccommodationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.ChangeRoom
{
    public class ChangeRoomAccommodationDateCommand : IRequest<ChangeRoomAccommodationDateResponse>
    {
        public Guid Gid { get; set; }
        public Guid NewRoomNoGid { get; set; }

        public class ChangeRoomAccommodationDateCommandHandler : IRequestHandler<ChangeRoomAccommodationDateCommand, ChangeRoomAccommodationDateResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAccommodationDateWriteRepository _accommodationDateWriteRepository;
            private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
            private readonly AccommodationDateBusinessRules _accommodationDateBusinessRules;
            private readonly IUnitOfWork _unitOfWork;

            public ChangeRoomAccommodationDateCommandHandler(IMapper mapper, IAccommodationDateWriteRepository accommodationDateWriteRepository, IAccommodationDateReadRepository accommodationDateReadRepository, AccommodationDateBusinessRules accommodationDateBusinessRules, IUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _accommodationDateWriteRepository = accommodationDateWriteRepository;
                _accommodationDateReadRepository = accommodationDateReadRepository;
                _accommodationDateBusinessRules = accommodationDateBusinessRules;
                _unitOfWork = unitOfWork;
            }

            public async Task<ChangeRoomAccommodationDateResponse> Handle(ChangeRoomAccommodationDateCommand request, CancellationToken cancellationToken)
            {
                await _accommodationDateBusinessRules.IsRoomNoFKExist(request.NewRoomNoGid);


                AccommodationDate? accommodationDate = await _accommodationDateReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.ReservationDetailFK).Include(x => x.ReservationDetailFK).ThenInclude(x => x.RoomTypeFK).Include(x => x.ReservationRoomFK));
                await _accommodationDateBusinessRules.AccommodationDateShouldExistWhenSelected(accommodationDate);
                accommodationDate = _mapper.Map(request, accommodationDate);


                _unitOfWork.BeginTransaction();
                try
                {
                    AccommodationDate newAccommodationDate = new()
                    {
                        Date = accommodationDate.Date,
                        GidGuestFK = accommodationDate.GidGuestFK,
                        GidReservationDetailFK = accommodationDate.GidReservationDetailFK,
                        GidRoomNoFK = request.NewRoomNoGid,
                        PreviousRoomInfo = accommodationDate.ReservationDetailFK.RoomTypeFK.Name + "-" + accommodationDate.ReservationRoomFK.RoomNo
                            + "_" + accommodationDate.GidRoomNoFK,
                    };

                    await _accommodationDateBusinessRules.ValidateRoomCapacity(newAccommodationDate.GidReservationDetailFK, newAccommodationDate.GidRoomNoFK.Value, new DateTime[] { newAccommodationDate.Date });
                    await _accommodationDateBusinessRules.ValidateRoomTypeAvailability(newAccommodationDate.GidReservationDetailFK, new DateTime[] { newAccommodationDate.Date }, newAccommodationDate.GidRoomNoFK.Value);


                    accommodationDate.DataState = DataState.Deleted;
                    _accommodationDateWriteRepository.Add(newAccommodationDate);
                    _accommodationDateWriteRepository.Update(accommodationDate);

                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                    throw new BusinessException("Transaction sırasında bir hata oluştu.", ex);
                }
                finally
                {
                    _unitOfWork.Dispose();
                }

                return new()
                {
                    Title = AccommodationDatesBusinessMessages.ProcessCompleted,
                    Message = AccommodationDatesBusinessMessages.SuccessCreatedAccommodationDateMessage,
                    IsValid = true
                };

            }
        }
    }
}
