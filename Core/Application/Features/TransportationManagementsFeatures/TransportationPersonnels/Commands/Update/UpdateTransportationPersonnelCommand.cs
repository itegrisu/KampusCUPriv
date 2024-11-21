using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Rules;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Update;

public class UpdateTransportationPersonnelCommand : IRequest<UpdatedTransportationPersonnelResponse>
{
    public Guid Gid { get; set; }

	public Guid GidTransportationServiceFK { get; set; }
public Guid GidStaffPersonnelFK { get; set; }

public EnumStaffType StaffType { get; set; }
public string? Description { get; set; }



    public class UpdateTransportationPersonnelCommandHandler : IRequestHandler<UpdateTransportationPersonnelCommand, UpdatedTransportationPersonnelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationPersonnelWriteRepository _transportationPersonnelWriteRepository;
        private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
        private readonly TransportationPersonnelBusinessRules _transportationPersonnelBusinessRules;

        public UpdateTransportationPersonnelCommandHandler(IMapper mapper, ITransportationPersonnelWriteRepository transportationPersonnelWriteRepository,
                                         TransportationPersonnelBusinessRules transportationPersonnelBusinessRules, ITransportationPersonnelReadRepository transportationPersonnelReadRepository)
        {
            _mapper = mapper;
            _transportationPersonnelWriteRepository = transportationPersonnelWriteRepository;
            _transportationPersonnelBusinessRules = transportationPersonnelBusinessRules;
            _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
        }

        public async Task<UpdatedTransportationPersonnelResponse> Handle(UpdateTransportationPersonnelCommand request, CancellationToken cancellationToken)
        {
            X.TransportationPersonnel? transportationPersonnel = await _transportationPersonnelReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.TransportationServiceFK).Include(x => x.UserFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _transportationPersonnelBusinessRules.TransportationPersonnelShouldExistWhenSelected(transportationPersonnel);
            transportationPersonnel = _mapper.Map(request, transportationPersonnel);

            _transportationPersonnelWriteRepository.Update(transportationPersonnel!);
            await _transportationPersonnelWriteRepository.SaveAsync();
            GetByGidTransportationPersonnelResponse obj = _mapper.Map<GetByGidTransportationPersonnelResponse>(transportationPersonnel);

            return new()
            {
                Title = TransportationPersonnelsBusinessMessages.ProcessCompleted,
                Message = TransportationPersonnelsBusinessMessages.SuccessCreatedTransportationPersonnelMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}