using Application.Features.OfferManagementFeatures.OfferFiles.Constants;
using Application.Features.OfferManagementFeatures.OfferFiles.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.OfferFiles.Rules;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.Update;

public class UpdateOfferFileCommand : IRequest<UpdatedOfferFileResponse>
{
    public Guid Gid { get; set; }

    public Guid GidOfferFK { get; set; }

    public string Title { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class UpdateOfferFileCommandHandler : IRequestHandler<UpdateOfferFileCommand, UpdatedOfferFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferFileWriteRepository _offerFileWriteRepository;
        private readonly IOfferFileReadRepository _offerFileReadRepository;
        private readonly OfferFileBusinessRules _offerFileBusinessRules;

        public UpdateOfferFileCommandHandler(IMapper mapper, IOfferFileWriteRepository offerFileWriteRepository,
                                         OfferFileBusinessRules offerFileBusinessRules, IOfferFileReadRepository offerFileReadRepository)
        {
            _mapper = mapper;
            _offerFileWriteRepository = offerFileWriteRepository;
            _offerFileBusinessRules = offerFileBusinessRules;
            _offerFileReadRepository = offerFileReadRepository;
        }

        public async Task<UpdatedOfferFileResponse> Handle(UpdateOfferFileCommand request, CancellationToken cancellationToken)
        {
            X.OfferFile? offerFile = await _offerFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _offerFileBusinessRules.OfferFileShouldExistWhenSelected(offerFile);
            await _offerFileBusinessRules.OfferShouldExistWhenSelected(request.GidOfferFK);
            offerFile = _mapper.Map(request, offerFile);

            _offerFileWriteRepository.Update(offerFile!);
            await _offerFileWriteRepository.SaveAsync();

            X.OfferFile updatedOfferFile = await _offerFileReadRepository.GetAsync(predicate: x => x.Gid == offerFile.Gid, include: x => x.Include(x => x.OfferFK));

            GetByGidOfferFileResponse obj = _mapper.Map<GetByGidOfferFileResponse>(updatedOfferFile);

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