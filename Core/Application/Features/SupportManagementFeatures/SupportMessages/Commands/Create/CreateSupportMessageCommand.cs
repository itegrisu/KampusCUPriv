using Application.Features.SupportManagementFeatures.SupportMessages.Constants;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportMessages.Rules;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using AutoMapper;
using X = Domain.Entities.SupportManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.Create;

public class CreateSupportMessageCommand : IRequest<CreatedSupportMessageResponse>
{
    public Guid GidSupportFK { get; set; }
    public Guid GidSenderUserFK { get; set; }
    public string Message { get; set; }



    public class CreateSupportMessageCommandHandler : IRequestHandler<CreateSupportMessageCommand, CreatedSupportMessageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportMessageWriteRepository _supportMessageWriteRepository;
        private readonly ISupportMessageReadRepository _supportMessageReadRepository;
        private readonly SupportMessageBusinessRules _supportMessageBusinessRules;

        public CreateSupportMessageCommandHandler(IMapper mapper, ISupportMessageWriteRepository supportMessageWriteRepository,
                                         SupportMessageBusinessRules supportMessageBusinessRules, ISupportMessageReadRepository supportMessageReadRepository)
        {
            _mapper = mapper;
            _supportMessageWriteRepository = supportMessageWriteRepository;
            _supportMessageBusinessRules = supportMessageBusinessRules;
            _supportMessageReadRepository = supportMessageReadRepository;
        }

        public async Task<CreatedSupportMessageResponse> Handle(CreateSupportMessageCommand request, CancellationToken cancellationToken)
        {
           await _supportMessageBusinessRules.SupportRequestShouldExistWhenSelected(request.GidSupportFK);
           await _supportMessageBusinessRules.UserShouldExistWhenSelected(request.GidSenderUserFK);


            X.SupportMessage supportMessage = _mapper.Map<X.SupportMessage>(request);
            supportMessage.MessageType = EnumMessageType.Message;

            await _supportMessageWriteRepository.AddAsync(supportMessage);
            await _supportMessageWriteRepository.SaveAsync();

            X.SupportMessage savedSupportMessage = await _supportMessageReadRepository.GetAsync(predicate: x => x.Gid == supportMessage.Gid,
                include: i=>i.Include(i=>i.UserFK).Include(i=>i.SupportRequestFK));
          

            GetByGidSupportMessageResponse obj = _mapper.Map<GetByGidSupportMessageResponse>(savedSupportMessage);
            return new()
            {
                Title = SupportMessagesBusinessMessages.ProcessCompleted,
                Message = SupportMessagesBusinessMessages.SuccessCreatedSupportMessageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}