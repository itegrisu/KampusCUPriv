using Application.Features.StockManagementFeatures.StockCardImages.Constants;
using Application.Features.StockManagementFeatures.StockCardImages.Queries.GetByGid;
using Application.Features.StockManagementFeatures.StockCardImages.Rules;
using Application.Repositories.StockManagementRepos.StockCardImageRepo;
using AutoMapper;
using X = Domain.Entities.StockManagements;
using MediatR;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.Update;

public class UpdateStockCardImageCommand : IRequest<UpdatedStockCardImageResponse>
{
    public Guid Gid { get; set; }
    public Guid GidStockCardFK { get; set; }
    public string Title { get; set; }
    public string? Image { get; set; }

    public class UpdateStockCardImageCommandHandler : IRequestHandler<UpdateStockCardImageCommand, UpdatedStockCardImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCardImageWriteRepository _stockCardImageWriteRepository;
        private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
        private readonly StockCardImageBusinessRules _stockCardImageBusinessRules;

        public UpdateStockCardImageCommandHandler(IMapper mapper, IStockCardImageWriteRepository stockCardImageWriteRepository,
                                         StockCardImageBusinessRules stockCardImageBusinessRules, IStockCardImageReadRepository stockCardImageReadRepository)
        {
            _mapper = mapper;
            _stockCardImageWriteRepository = stockCardImageWriteRepository;
            _stockCardImageBusinessRules = stockCardImageBusinessRules;
            _stockCardImageReadRepository = stockCardImageReadRepository;
        }

        public async Task<UpdatedStockCardImageResponse> Handle(UpdateStockCardImageCommand request, CancellationToken cancellationToken)
        {
            X.StockCardImage? stockCardImage = await _stockCardImageReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _stockCardImageBusinessRules.StockCardImageShouldExistWhenSelected(stockCardImage);
            await _stockCardImageBusinessRules.StockCardShouldExistWhenSelected(request.GidStockCardFK);
            stockCardImage = _mapper.Map(request, stockCardImage);

            _stockCardImageWriteRepository.Update(stockCardImage!);
            await _stockCardImageWriteRepository.SaveAsync();
            GetByGidStockCardImageResponse obj = _mapper.Map<GetByGidStockCardImageResponse>(stockCardImage);

            return new()
            {
                Title = StockCardImagesBusinessMessages.ProcessCompleted,
                Message = StockCardImagesBusinessMessages.SuccessCreatedStockCardImageMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}