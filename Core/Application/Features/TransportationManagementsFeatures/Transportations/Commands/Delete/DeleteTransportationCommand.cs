using Application.Features.TransportationManagementFeatures.Transportations.Constants;
using Application.Features.TransportationManagementFeatures.Transportations.Rules;
using Application.Repositories.TransportationRepos.TransportationRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.Transportations.Commands.Delete;

public class DeleteTransportationCommand : IRequest<DeletedTransportationResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTransportationCommandHandler : IRequestHandler<DeleteTransportationCommand, DeletedTransportationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationReadRepository _transportationReadRepository;
        private readonly ITransportationWriteRepository _transportationWriteRepository;
        private readonly TransportationBusinessRules _transportationBusinessRules;

        public DeleteTransportationCommandHandler(IMapper mapper, ITransportationReadRepository transportationReadRepository,
                                         TransportationBusinessRules transportationBusinessRules, ITransportationWriteRepository transportationWriteRepository)
        {
            _mapper = mapper;
            _transportationReadRepository = transportationReadRepository;
            _transportationBusinessRules = transportationBusinessRules;
            _transportationWriteRepository = transportationWriteRepository;
        }

        public async Task<DeletedTransportationResponse> Handle(DeleteTransportationCommand request, CancellationToken cancellationToken)
        {
            X.Transportation? transportation = await _transportationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _transportationBusinessRules.TransportationShouldExistWhenSelected(transportation);
            transportation.DataState = Core.Enum.DataState.Deleted;

            _transportationWriteRepository.Update(transportation);
            await _transportationWriteRepository.SaveAsync();

            return new()
            {
                Title = TransportationsBusinessMessages.ProcessCompleted,
                Message = TransportationsBusinessMessages.SuccessDeletedTransportationMessage,
                IsValid = true
            };
        }
    }
}