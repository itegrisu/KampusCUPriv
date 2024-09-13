using Application.Features.PortalManagementFeatures.PortalTexts.Constants;
using Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetByGid;
using Application.Features.PortalManagementFeatures.PortalTexts.Rules;
using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using AutoMapper;
using X = Domain.Entities.PortalManagements;
using MediatR;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Commands.Update;

public class UpdatePortalTextCommand : IRequest<UpdatedPortalTextResponse>
{
    public Guid Gid { get; set; }

	
public string Title { get; set; }
public string? Content { get; set; }
public string? Description { get; set; }
public bool IsRichTextBox { get; set; }
public string? ContentRich { get; set; }



    public class UpdatePortalTextCommandHandler : IRequestHandler<UpdatePortalTextCommand, UpdatedPortalTextResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPortalTextWriteRepository _portalTextWriteRepository;
        private readonly IPortalTextReadRepository _portalTextReadRepository;
        private readonly PortalTextBusinessRules _portalTextBusinessRules;

        public UpdatePortalTextCommandHandler(IMapper mapper, IPortalTextWriteRepository portalTextWriteRepository,
                                         PortalTextBusinessRules portalTextBusinessRules, IPortalTextReadRepository portalTextReadRepository)
        {
            _mapper = mapper;
            _portalTextWriteRepository = portalTextWriteRepository;
            _portalTextBusinessRules = portalTextBusinessRules;
            _portalTextReadRepository = portalTextReadRepository;
        }

        public async Task<UpdatedPortalTextResponse> Handle(UpdatePortalTextCommand request, CancellationToken cancellationToken)
        {
            X.PortalText? portalText = await _portalTextReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _portalTextBusinessRules.PortalTextShouldExistWhenSelected(portalText);
            portalText = _mapper.Map(request, portalText);

            _portalTextWriteRepository.Update(portalText!);
            await _portalTextWriteRepository.SaveAsync();
            GetByGidPortalTextResponse obj = _mapper.Map<GetByGidPortalTextResponse>(portalText);

            return new()
            {
                Title = PortalTextsBusinessMessages.ProcessCompleted,
                Message = PortalTextsBusinessMessages.SuccessUpdatedPortalTextMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}