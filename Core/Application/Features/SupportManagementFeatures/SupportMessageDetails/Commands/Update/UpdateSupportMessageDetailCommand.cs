using Application.Features.SupportManagementFeatures.SupportMessageDetails.Constants;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Rules;
using Application.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using AutoMapper;
using X = Domain.Entities.SupportManagements;
using MediatR;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Update;

public class UpdateSupportMessageDetailCommand : IRequest<UpdatedSupportMessageDetailResponse>
{
    public Guid Gid { get; set; }

	public Guid GidSupportFK { get; set; }
public Guid GidReadUserFK { get; set; }

public DateTime ReadDate { get; set; }
public string ReadIp { get; set; }



    public class UpdateSupportMessageDetailCommandHandler : IRequestHandler<UpdateSupportMessageDetailCommand, UpdatedSupportMessageDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportMessageDetailWriteRepository _supportMessageDetailWriteRepository;
        private readonly ISupportMessageDetailReadRepository _supportMessageDetailReadRepository;
        private readonly SupportMessageDetailBusinessRules _supportMessageDetailBusinessRules;

        public UpdateSupportMessageDetailCommandHandler(IMapper mapper, ISupportMessageDetailWriteRepository supportMessageDetailWriteRepository,
                                         SupportMessageDetailBusinessRules supportMessageDetailBusinessRules, ISupportMessageDetailReadRepository supportMessageDetailReadRepository)
        {
            _mapper = mapper;
            _supportMessageDetailWriteRepository = supportMessageDetailWriteRepository;
            _supportMessageDetailBusinessRules = supportMessageDetailBusinessRules;
            _supportMessageDetailReadRepository = supportMessageDetailReadRepository;
        }

        public async Task<UpdatedSupportMessageDetailResponse> Handle(UpdateSupportMessageDetailCommand request, CancellationToken cancellationToken)
        {

            X.SupportMessageDetail? supportMessageDetail = await _supportMessageDetailReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _supportMessageDetailBusinessRules.UserShouldExistWhenSelected(request.GidReadUserFK);
            await _supportMessageDetailBusinessRules.SupportRequestShouldExistWhenSelected(request.GidSupportFK);
            
            await _supportMessageDetailBusinessRules.SupportMessageDetailShouldExistWhenSelected(supportMessageDetail);
            supportMessageDetail = _mapper.Map(request, supportMessageDetail);

            _supportMessageDetailWriteRepository.Update(supportMessageDetail!);
            await _supportMessageDetailWriteRepository.SaveAsync();
            GetByGidSupportMessageDetailResponse obj = _mapper.Map<GetByGidSupportMessageDetailResponse>(supportMessageDetail);

            return new()
            {
                Title = SupportMessageDetailsBusinessMessages.ProcessCompleted,
                Message = SupportMessageDetailsBusinessMessages.SuccessUpdatedSupportMessageDetailMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}