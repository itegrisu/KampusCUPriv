using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Constants;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Rules;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using AutoMapper;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.TransportationManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Update;

public class UpdateTransportationGroupCommand : IRequest<UpdatedTransportationGroupResponse>
{
    public Guid Gid { get; set; }

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
    public string? RefNoTransportationGroup { get; set; }

    public class UpdateTransportationGroupCommandHandler : IRequestHandler<UpdateTransportationGroupCommand, UpdatedTransportationGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationGroupWriteRepository _transportationGroupWriteRepository;
        private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
        private readonly TransportationGroupBusinessRules _transportationGroupBusinessRules;
        private readonly IUlasýmService _ulasýmService;
        public UpdateTransportationGroupCommandHandler(IMapper mapper, ITransportationGroupWriteRepository transportationGroupWriteRepository,
                                         TransportationGroupBusinessRules transportationGroupBusinessRules, ITransportationGroupReadRepository transportationGroupReadRepository, IUlasýmService ulasýmService)
        {
            _mapper = mapper;
            _transportationGroupWriteRepository = transportationGroupWriteRepository;
            _transportationGroupBusinessRules = transportationGroupBusinessRules;
            _transportationGroupReadRepository = transportationGroupReadRepository;
            _ulasýmService = ulasýmService;
        }

        public async Task<UpdatedTransportationGroupResponse> Handle(UpdateTransportationGroupCommand request, CancellationToken cancellationToken)
        {
            X.TransportationGroup? transportationGroup = await _transportationGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.StartCountryFK).Include(x => x.StartCityFK).Include(x => x.StartDistrictFK).Include(x => x.EndCountryFK).Include(x => x.EndCityFK).Include(x => x.EndDistrictFK).Include(x => x.TransportationServiceFK).Include(x => x.TransportationPassengers));

            await _transportationGroupBusinessRules.TransportationGroupShouldExistWhenSelected(transportationGroup);
            transportationGroup = _mapper.Map(request, transportationGroup);
      
            if (!request.RefNoTransportationGroup.IsNullOrEmpty())
            {
                var responseGrup = await _ulasýmService.GrupGuncelleAsync(transportationGroup, long.Parse(transportationGroup.TransportationServiceFK.RefNoTransportation), long.Parse(transportationGroup.RefNoTransportationGroup));

                if (!responseGrup.Contains("HATA"))
                {
                    transportationGroup.RefNoTransportationGroup = responseGrup;
                }
                else
                {
                    return new()
                    {
                        Title = TransportationGroupsBusinessMessages.TechnicalError,
                        Message = responseGrup,
                        IsValid = false,
                    };
                }
            }
                

            _transportationGroupWriteRepository.Update(transportationGroup!);
            await _transportationGroupWriteRepository.SaveAsync();
            GetByGidTransportationGroupResponse obj = _mapper.Map<GetByGidTransportationGroupResponse>(transportationGroup);

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