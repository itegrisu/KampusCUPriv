using Application.Features.SupportManagementFeatures.SupportRequests.Constants;
using Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportRequests.Rules;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using AutoMapper;
using X = Domain.Entities.SupportManagements;
using MediatR;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Commands.Update;

public class UpdateSupportRequestCommand : IRequest<UpdatedSupportRequestResponse>
{
    public Guid Gid { get; set; }
    public string Title { get; set; }
    public EnumSupportStatus SupportStatus { get; set; }
    public EnumPriorityType PriorityType { get; set; }
    public EnumSupportType SupportType { get; set; }



    public class UpdateSupportRequestCommandHandler : IRequestHandler<UpdateSupportRequestCommand, UpdatedSupportRequestResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportRequestWriteRepository _supportRequestWriteRepository;
        private readonly ISupportRequestReadRepository _supportRequestReadRepository;
        private readonly SupportRequestBusinessRules _supportRequestBusinessRules;

        public UpdateSupportRequestCommandHandler(IMapper mapper, ISupportRequestWriteRepository supportRequestWriteRepository,
                                         SupportRequestBusinessRules supportRequestBusinessRules, ISupportRequestReadRepository supportRequestReadRepository)
        {
            _mapper = mapper;
            _supportRequestWriteRepository = supportRequestWriteRepository;
            _supportRequestBusinessRules = supportRequestBusinessRules;
            _supportRequestReadRepository = supportRequestReadRepository;
        }

        public async Task<UpdatedSupportRequestResponse> Handle(UpdateSupportRequestCommand request, CancellationToken cancellationToken)
        {
            X.SupportRequest? supportRequest = await _supportRequestReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _supportRequestBusinessRules.SupportRequestShouldExistWhenSelected(supportRequest);
            supportRequest = _mapper.Map(request, supportRequest);

            _supportRequestWriteRepository.Update(supportRequest!);
            await _supportRequestWriteRepository.SaveAsync();

            X.SupportRequest? savedSupportRequest = await _supportRequestReadRepository.GetAsync(predicate: x => x.Gid == supportRequest.Gid,
                include: x => x.Include(i => i.UserFK));

            GetByGidSupportRequestResponse obj = _mapper.Map<GetByGidSupportRequestResponse>(savedSupportRequest);

            return new()
            {
                Title = SupportRequestsBusinessMessages.ProcessCompleted,
                Message = SupportRequestsBusinessMessages.SuccessUpdatedSupportRequestMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}