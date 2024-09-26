using Application.Features.SupportManagementFeatures.SupportMessages.Constants;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportMessages.Rules;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using AutoMapper;
using X = Domain.Entities.SupportManagements;
using MediatR;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.Update;

public class UpdateSupportMessageCommand : IRequest<UpdatedSupportMessageResponse>
{
    public Guid Gid { get; set; }
    public Guid GidSupportFK { get; set; }
    public Guid GidSenderUserFK { get; set; }
    public string Message { get; set; }
    public EnumMessageType MessageType { get; set; }

    public class UpdateSupportMessageCommandHandler : IRequestHandler<UpdateSupportMessageCommand, UpdatedSupportMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportMessageWriteRepository _supportMessageWriteRepository;
        private readonly ISupportMessageReadRepository _supportMessageReadRepository;
        private readonly SupportMessageBusinessRules _supportMessageBusinessRules;

        public UpdateSupportMessageCommandHandler(IMapper mapper, ISupportMessageWriteRepository supportMessageWriteRepository,
                                         SupportMessageBusinessRules supportMessageBusinessRules, ISupportMessageReadRepository supportMessageReadRepository)
        {
            _mapper = mapper;
            _supportMessageWriteRepository = supportMessageWriteRepository;
            _supportMessageBusinessRules = supportMessageBusinessRules;
            _supportMessageReadRepository = supportMessageReadRepository;
        }

        public async Task<UpdatedSupportMessageResponse> Handle(UpdateSupportMessageCommand request, CancellationToken cancellationToken)
        {
            X.SupportMessage? supportMessage = await _supportMessageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken,
                include:i=>i.Include(i=>i.UserFK).Include(i=>i.SupportRequestFK));
            
            await _supportMessageBusinessRules.SupportMessageShouldExistWhenSelected(supportMessage);
            await _supportMessageBusinessRules.SupportRequestShouldExistWhenSelected(request.GidSupportFK);
            await _supportMessageBusinessRules.UserShouldExistWhenSelected(request.GidSenderUserFK);
            supportMessage = _mapper.Map(request, supportMessage);

            _supportMessageWriteRepository.Update(supportMessage!);
            await _supportMessageWriteRepository.SaveAsync();
            GetByGidSupportMessageResponse obj = _mapper.Map<GetByGidSupportMessageResponse>(supportMessage);

            return new()
            {
                Title = SupportMessagesBusinessMessages.ProcessCompleted,
                Message = SupportMessagesBusinessMessages.SuccessUpdatedSupportMessageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}