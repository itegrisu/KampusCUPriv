using Application.Features.AccommodationManagementFeatures.AccommodationDates.Constants;
using Application.Features.AccommodationManagementFeatures.AccommodationDates.Rules;
using AutoMapper;
using X = Domain.Entities.AccommodationManagements;
using MediatR;
using Application.Repositories.AccommodationManagements.AccommodationDateRepo;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Delete;

public class DeleteAccommodationDateCommand : IRequest<DeletedAccommodationDateResponse>
{
	public Guid Gid { get; set; }

    public class DeleteAccommodationDateCommandHandler : IRequestHandler<DeleteAccommodationDateCommand, DeletedAccommodationDateResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccommodationDateReadRepository _accommodationDateReadRepository;
        private readonly IAccommodationDateWriteRepository _accommodationDateWriteRepository;
        private readonly AccommodationDateBusinessRules _accommodationDateBusinessRules;

        public DeleteAccommodationDateCommandHandler(IMapper mapper, IAccommodationDateReadRepository accommodationDateReadRepository,
                                         AccommodationDateBusinessRules accommodationDateBusinessRules, IAccommodationDateWriteRepository accommodationDateWriteRepository)
        {
            _mapper = mapper;
            _accommodationDateReadRepository = accommodationDateReadRepository;
            _accommodationDateBusinessRules = accommodationDateBusinessRules;
            _accommodationDateWriteRepository = accommodationDateWriteRepository;
        }

        public async Task<DeletedAccommodationDateResponse> Handle(DeleteAccommodationDateCommand request, CancellationToken cancellationToken)
        {
            X.AccommodationDate? accommodationDate = await _accommodationDateReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _accommodationDateBusinessRules.AccommodationDateShouldExistWhenSelected(accommodationDate);
            accommodationDate.DataState = Core.Enum.DataState.Deleted;

            _accommodationDateWriteRepository.Update(accommodationDate);
            await _accommodationDateWriteRepository.SaveAsync();

            return new()
            {
                Title = AccommodationDatesBusinessMessages.ProcessCompleted,
                Message = AccommodationDatesBusinessMessages.SuccessDeletedAccommodationDateMessage,
                IsValid = true
            };
        }
    }
}