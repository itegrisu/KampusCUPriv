using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Constants;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Rules;
using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Create;

public class CreateTransportationPersonnelCommand : IRequest<CreatedTransportationPersonnelResponse>
{
    public Guid GidTransportationServiceFK { get; set; }
    public Guid GidStaffPersonnelFK { get; set; }
    public EnumStaffType StaffType { get; set; }
    public EnumStaffStatus StaffStatus { get; set; }
    public string? Description { get; set; }



    public class CreateTransportationPersonnelCommandHandler : IRequestHandler<CreateTransportationPersonnelCommand, CreatedTransportationPersonnelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationPersonnelWriteRepository _transportationPersonnelWriteRepository;
        private readonly ITransportationPersonnelReadRepository _transportationPersonnelReadRepository;
        private readonly TransportationPersonnelBusinessRules _transportationPersonnelBusinessRules;

        public CreateTransportationPersonnelCommandHandler(IMapper mapper, ITransportationPersonnelWriteRepository transportationPersonnelWriteRepository,
                                         TransportationPersonnelBusinessRules transportationPersonnelBusinessRules, ITransportationPersonnelReadRepository transportationPersonnelReadRepository)
        {
            _mapper = mapper;
            _transportationPersonnelWriteRepository = transportationPersonnelWriteRepository;
            _transportationPersonnelBusinessRules = transportationPersonnelBusinessRules;
            _transportationPersonnelReadRepository = transportationPersonnelReadRepository;
        }

        public async Task<CreatedTransportationPersonnelResponse> Handle(CreateTransportationPersonnelCommand request, CancellationToken cancellationToken)
        {
            await _transportationPersonnelBusinessRules.PersonnelAllreadyExist(request.GidStaffPersonnelFK);

            //int maxRowNo = await _transportationPersonnelReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.TransportationPersonnel transportationPersonnel = _mapper.Map<X.TransportationPersonnel>(request);
            //transportationPersonnel.RowNo = maxRowNo + 1;

            await _transportationPersonnelWriteRepository.AddAsync(transportationPersonnel);
            await _transportationPersonnelWriteRepository.SaveAsync();

            X.TransportationPersonnel savedTransportationPersonnel = await _transportationPersonnelReadRepository.GetAsync(predicate: x => x.Gid == transportationPersonnel.Gid,
                include: x => x.Include(x => x.TransportationServiceFK).Include(x => x.UserFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidTransportationPersonnelResponse obj = _mapper.Map<GetByGidTransportationPersonnelResponse>(savedTransportationPersonnel);
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