using Application.Features.PortalManagementFeatures.PortalTexts.Constants;
using Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetByGid;
using Application.Features.PortalManagementFeatures.PortalTexts.Rules;
using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using AutoMapper;
using X = Domain.Entities.PortalManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Commands.Create;

public class CreatePortalTextCommand : IRequest<CreatedPortalTextResponse>
{
    
public string Title { get; set; }
public string? Content { get; set; }
public string? Description { get; set; }
public bool IsRichTextBox { get; set; }
public string? ContentRich { get; set; }



    public class CreatePortalTextCommandHandler : IRequestHandler<CreatePortalTextCommand, CreatedPortalTextResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPortalTextWriteRepository _portalTextWriteRepository;
        private readonly IPortalTextReadRepository _portalTextReadRepository;
        private readonly PortalTextBusinessRules _portalTextBusinessRules;

        public CreatePortalTextCommandHandler(IMapper mapper, IPortalTextWriteRepository portalTextWriteRepository,
                                         PortalTextBusinessRules portalTextBusinessRules, IPortalTextReadRepository portalTextReadRepository)
        {
            _mapper = mapper;
            _portalTextWriteRepository = portalTextWriteRepository;
            _portalTextBusinessRules = portalTextBusinessRules;
            _portalTextReadRepository = portalTextReadRepository;
        }

        public async Task<CreatedPortalTextResponse> Handle(CreatePortalTextCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _portalTextReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.PortalText portalText = _mapper.Map<X.PortalText>(request);
            //portalText.RowNo = maxRowNo + 1;

            await _portalTextWriteRepository.AddAsync(portalText);
            await _portalTextWriteRepository.SaveAsync();

			X.PortalText savedPortalText = await _portalTextReadRepository.GetAsync(predicate: x => x.Gid == portalText.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidPortalTextResponse obj = _mapper.Map<GetByGidPortalTextResponse>(savedPortalText);
            return new()
            {           
                Title = PortalTextsBusinessMessages.ProcessCompleted,
                Message = PortalTextsBusinessMessages.SuccessCreatedPortalTextMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}