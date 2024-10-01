using Application.Features.OfferManagementFeatures.OfferFiles.Constants;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.OfferFiles.Rules;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.Create;

public class CreateOfferFileCommand : IRequest<CreatedOfferFileResponse>
{
    public Guid GidOfferFK { get; set; }

    public string Title { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class CreateOfferFileCommandHandler : IRequestHandler<CreateOfferFileCommand, CreatedOfferFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferFileWriteRepository _offerFileWriteRepository;
        private readonly IOfferFileReadRepository _offerFileReadRepository;
        private readonly OfferFileBusinessRules _offerFileBusinessRules;

        public CreateOfferFileCommandHandler(IMapper mapper, IOfferFileWriteRepository offerFileWriteRepository,
                                         OfferFileBusinessRules offerFileBusinessRules, IOfferFileReadRepository offerFileReadRepository)
        {
            _mapper = mapper;
            _offerFileWriteRepository = offerFileWriteRepository;
            _offerFileBusinessRules = offerFileBusinessRules;
            _offerFileReadRepository = offerFileReadRepository;
        }

        public async Task<CreatedOfferFileResponse> Handle(CreateOfferFileCommand request, CancellationToken cancellationToken)
        {
            await _offerFileBusinessRules.OfferShouldExistWhenSelected(request.GidOfferFK);

            X.OfferFile offerFile = _mapper.Map<X.OfferFile>(request);

            await _offerFileWriteRepository.AddAsync(offerFile);
            await _offerFileWriteRepository.SaveAsync();

            X.OfferFile savedOfferFile = await _offerFileReadRepository.GetAsync(predicate: x => x.Gid == offerFile.Gid, include: x => x.Include(x => x.OfferFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidOfferFileResponse obj = _mapper.Map<GetByGidOfferFileResponse>(savedOfferFile);
            return new()
            {
                Title = OfferFilesBusinessMessages.ProcessCompleted,
                Message = OfferFilesBusinessMessages.SuccessCreatedOfferFileMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}