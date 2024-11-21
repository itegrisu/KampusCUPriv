using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Rules;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Delete;

public class DeleteTransportationPersonnelCommand : IRequest<DeletedTransportationPersonnelResponse>
{
	public Guid Gid { get; set; }

    public class DeleteTransportationPersonnelCommandHandler : IRequestHandler<DeleteTransportationPersonnelCommand, DeletedTransportationPersonnelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
        private readonly ITransportationPersonnelWriteRepository _transportationPersonnelWriteRepository;
        private readonly TransportationPersonnelBusinessRules _transportationPersonnelBusinessRules;

        public DeleteTransportationPersonnelCommandHandler(IMapper mapper, ITransportationPersonnelReadRepository transportationPersonnelReadRepository,
                                         TransportationPersonnelBusinessRules transportationPersonnelBusinessRules, ITransportationPersonnelWriteRepository transportationPersonnelWriteRepository)
        {
            _mapper = mapper;
            _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
            _transportationPersonnelBusinessRules = transportationPersonnelBusinessRules;
            _transportationPersonnelWriteRepository = transportationPersonnelWriteRepository;
        }

        public async Task<DeletedTransportationPersonnelResponse> Handle(DeleteTransportationPersonnelCommand request, CancellationToken cancellationToken)
        {
            X.TransportationPersonnel? transportationPersonnel = await _transportationPersonnelReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationServiceFK).Include(x => x.UserFK));
            await _transportationPersonnelBusinessRules.TransportationPersonnelShouldExistWhenSelected(transportationPersonnel);
            transportationPersonnel.DataState = Core.Enum.DataState.Deleted;

            _transportationPersonnelWriteRepository.Update(transportationPersonnel);
            await _transportationPersonnelWriteRepository.SaveAsync();

            return new()
            {
                Title = TransportationPersonnelsBusinessMessages.ProcessCompleted,
                Message = TransportationPersonnelsBusinessMessages.SuccessDeletedTransportationPersonnelMessage,
                IsValid = true
            };
        }
    }
}