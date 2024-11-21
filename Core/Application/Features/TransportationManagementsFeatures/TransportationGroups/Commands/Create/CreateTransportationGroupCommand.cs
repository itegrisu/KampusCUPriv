using Application.Features.TransportationManagementFeatures.TransportationGroups.Constants;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Rules;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Create;

public class CreateTransportationGroupCommand : IRequest<CreatedTransportationGroupResponse>
{
    public Guid GidTransportationServiceFK { get; set; }
    public Guid GidStartCountryFK { get; set; }
    public Guid GidStartCityFK { get; set; }
    public Guid GidStartDistrictFK { get; set; }
    public Guid GidEndCountryFK { get; set; }
    public Guid GidEndCityFK { get; set; }
    public Guid GidEndDistrictFK { get; set; }
    public string GroupName { get; set; }
    public decimal TransportationFee { get; set; }
    public string StartPlace { get; set; }
    public string EndPlace { get; set; }
    public string? Description { get; set; }



    public class CreateTransportationGroupCommandHandler : IRequestHandler<CreateTransportationGroupCommand, CreatedTransportationGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationGroupWriteRepository _transportationGroupWriteRepository;
        private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
        private readonly TransportationGroupBusinessRules _transportationGroupBusinessRules;

        public CreateTransportationGroupCommandHandler(IMapper mapper, ITransportationGroupWriteRepository transportationGroupWriteRepository,
                                         TransportationGroupBusinessRules transportationGroupBusinessRules, ITransportationGroupReadRepository transportationGroupReadRepository)
        {
            _mapper = mapper;
            _transportationGroupWriteRepository = transportationGroupWriteRepository;
            _transportationGroupBusinessRules = transportationGroupBusinessRules;
            _transportationGroupReadRepository = transportationGroupReadRepository;
        }

        public async Task<CreatedTransportationGroupResponse> Handle(CreateTransportationGroupCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _transportationGroupReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.TransportationGroup transportationGroup = _mapper.Map<X.TransportationGroup>(request);
            //transportationGroup.RowNo = maxRowNo + 1;

            await _transportationGroupWriteRepository.AddAsync(transportationGroup);
            await _transportationGroupWriteRepository.SaveAsync();

            X.TransportationGroup savedTransportationGroup = await _transportationGroupReadRepository.GetAsync(predicate: x => x.Gid == transportationGroup.Gid,
                include: x => x.Include(x => x.StartCountryFK).Include(x => x.StartCityFK).Include(x => x.StartDistrictFK).Include(x => x.EndCountryFK).Include(x => x.EndCityFK).Include(x => x.EndDistrictFK).Include(x => x.TransportationServiceFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidTransportationGroupResponse obj = _mapper.Map<GetByGidTransportationGroupResponse>(savedTransportationGroup);
            return new()
            {
                Title = TransportationGroupsBusinessMessages.ProcessCompleted,
                Message = TransportationGroupsBusinessMessages.SuccessCreatedTransportationGroupMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}