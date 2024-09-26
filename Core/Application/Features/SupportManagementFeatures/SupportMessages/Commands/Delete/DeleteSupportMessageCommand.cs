using Application.Features.SupportManagementFeatures.SupportMessages.Constants;
using Application.Features.SupportManagementFeatures.SupportMessages.Rules;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using AutoMapper;
using X = Domain.Entities.SupportManagements;
using MediatR;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.Delete;

public class DeleteSupportMessageCommand : IRequest<DeletedSupportMessageResponse>
{
	public Guid Gid { get; set; }

    public class DeleteSupportMessageCommandHandler : IRequestHandler<DeleteSupportMessageCommand, DeletedSupportMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportMessageReadRepository _supportMessageReadRepository;
        private readonly ISupportMessageWriteRepository _supportMessageWriteRepository;
        private readonly SupportMessageBusinessRules _supportMessageBusinessRules;

        public DeleteSupportMessageCommandHandler(IMapper mapper, ISupportMessageReadRepository supportMessageReadRepository,
                                         SupportMessageBusinessRules supportMessageBusinessRules, ISupportMessageWriteRepository supportMessageWriteRepository)
        {
            _mapper = mapper;
            _supportMessageReadRepository = supportMessageReadRepository;
            _supportMessageBusinessRules = supportMessageBusinessRules;
            _supportMessageWriteRepository = supportMessageWriteRepository;
        }

        public async Task<DeletedSupportMessageResponse> Handle(DeleteSupportMessageCommand request, CancellationToken cancellationToken)
        {
            X.SupportMessage? supportMessage = await _supportMessageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _supportMessageBusinessRules.SupportMessageShouldExistWhenSelected(supportMessage);
            supportMessage.DataState = Core.Enum.DataState.Deleted;

            _supportMessageWriteRepository.Update(supportMessage);
            await _supportMessageWriteRepository.SaveAsync();

            return new()
            {
                Title = SupportMessagesBusinessMessages.ProcessCompleted,
                Message = SupportMessagesBusinessMessages.SuccessDeletedSupportMessageMessage,
                IsValid = true
            };
        }
    }
}