using Application.Features.OfferManagementFeatures.OfferFiles.Constants;
using Application.Features.OfferManagementFeatures.OfferFiles.Rules;
using Application.Repositories.OfferManagementRepos.OfferFileRepo;
using AutoMapper;
using X = Domain.Entities.OfferManagements;
using MediatR;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.Delete;

public class DeleteOfferFileCommand : IRequest<DeletedOfferFileResponse>
{
	public Guid Gid { get; set; }

    public class DeleteOfferFileCommandHandler : IRequestHandler<DeleteOfferFileCommand, DeletedOfferFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferFileReadRepository _offerFileReadRepository;
        private readonly IOfferFileWriteRepository _offerFileWriteRepository;
        private readonly OfferFileBusinessRules _offerFileBusinessRules;

        public DeleteOfferFileCommandHandler(IMapper mapper, IOfferFileReadRepository offerFileReadRepository,
                                         OfferFileBusinessRules offerFileBusinessRules, IOfferFileWriteRepository offerFileWriteRepository)
        {
            _mapper = mapper;
            _offerFileReadRepository = offerFileReadRepository;
            _offerFileBusinessRules = offerFileBusinessRules;
            _offerFileWriteRepository = offerFileWriteRepository;
        }

        public async Task<DeletedOfferFileResponse> Handle(DeleteOfferFileCommand request, CancellationToken cancellationToken)
        {
            X.OfferFile? offerFile = await _offerFileReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _offerFileBusinessRules.OfferFileShouldExistWhenSelected(offerFile);
            offerFile.DataState = Core.Enum.DataState.Deleted;

            _offerFileWriteRepository.Update(offerFile);
            await _offerFileWriteRepository.SaveAsync();

            return new()
            {
                Title = OfferFilesBusinessMessages.ProcessCompleted,
                Message = OfferFilesBusinessMessages.SuccessDeletedOfferFileMessage,
                IsValid = true
            };
        }
    }
}