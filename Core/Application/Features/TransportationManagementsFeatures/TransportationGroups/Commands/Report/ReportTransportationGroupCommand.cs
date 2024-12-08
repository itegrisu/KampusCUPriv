using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Update;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Constants;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Rules;
using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;
namespace Application.Features.TransportationManagementsFeatures.TransportationGroups.Commands.Report
{
    public class ReportTransportationGroupCommand : IRequest<ReportedTransportationGroupResponse>
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

        public class ReportTransportationGroupCommandHandler : IRequestHandler<ReportTransportationGroupCommand, ReportedTransportationGroupResponse>
        {
            private readonly IMapper _mapper;
            private readonly ITransportationGroupWriteRepository _transportationGroupWriteRepository;
            private readonly ITransportationGroupReadRepository _transportationGroupReadRepository;
            private readonly TransportationGroupBusinessRules _transportationGroupBusinessRules;
            private readonly IUlasımService _ulasımService;

            public ReportTransportationGroupCommandHandler(IMapper mapper, ITransportationGroupWriteRepository transportationGroupWriteRepository,
                                             TransportationGroupBusinessRules transportationGroupBusinessRules, ITransportationGroupReadRepository transportationGroupReadRepository, IUlasımService ulasımService)
            {
                _mapper = mapper;
                _transportationGroupWriteRepository = transportationGroupWriteRepository;
                _transportationGroupBusinessRules = transportationGroupBusinessRules;
                _transportationGroupReadRepository = transportationGroupReadRepository;
                _ulasımService = ulasımService;
            }

            public async Task<ReportedTransportationGroupResponse> Handle(ReportTransportationGroupCommand request, CancellationToken cancellationToken)
            {
                X.TransportationGroup? transportationGroup = await _transportationGroupReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.StartCountryFK).Include(x => x.StartCityFK).Include(x => x.StartDistrictFK).Include(x => x.EndCountryFK).Include(x => x.EndCityFK).Include(x => x.EndDistrictFK).Include(x => x.TransportationServiceFK));
                //INCLUDES Buraya Gelecek include varsa eklenecek
                await _transportationGroupBusinessRules.TransportationGroupShouldExistWhenSelected(transportationGroup);
                transportationGroup = _mapper.Map(request, transportationGroup);

                var sonuc = await _ulasımService.GrupEkleAsync(transportationGroup);

                if (!sonuc.Contains("HATA"))
                {
                    transportationGroup.RefNoTransportationGroup = sonuc;
                    _transportationGroupWriteRepository.Update(transportationGroup!);
                    await _transportationGroupWriteRepository.SaveAsync();
                    GetByGidTransportationGroupResponse obj = _mapper.Map<GetByGidTransportationGroupResponse>(transportationGroup);

                    return new()
                    {
                        Title = TransportationGroupsBusinessMessages.ProcessCompleted,
                        Message = TransportationGroupsBusinessMessages.SuccessReportedTransportationGroupMessage,
                        IsValid = true,
                        Obj = obj
                    };
                }                
                    return new()
                    {
                        Title = TransportationGroupsBusinessMessages.TechnicalError,
                        Message = sonuc,
                        IsValid = false,
                    };              
            }
        }
    }
}
