using Application.Features.WareHouseManagementFeatures.StockCardImages.Constants;
using Application.Features.WareHouseManagementFeatures.StockCardImages.Queries.GetByGid;
using Application.Features.WareHouseManagementFeatures.StockCardImages.Rules;
using Application.Repositories.WarehouseManagementRepos.StockCardImageRepo;
using AutoMapper;
using Domain.Entities.WarehouseManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.Create;

public class CreateStockCardImageCommand : IRequest<CreatedStockCardImageResponse>
{
    public Guid GidStockCardFK { get; set; }
    public string Title { get; set; }

    public class CreateStockCardImageCommandHandler : IRequestHandler<CreateStockCardImageCommand, CreatedStockCardImageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStockCardImageWriteRepository _stockCardImageWriteRepository;
        private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
        private readonly StockCardImageBusinessRules _stockCardImageBusinessRules;

        public CreateStockCardImageCommandHandler(IMapper mapper, IStockCardImageWriteRepository stockCardImageWriteRepository,
                                         StockCardImageBusinessRules stockCardImageBusinessRules, IStockCardImageReadRepository stockCardImageReadRepository)
        {
            _mapper = mapper;
            _stockCardImageWriteRepository = stockCardImageWriteRepository;
            _stockCardImageBusinessRules = stockCardImageBusinessRules;
            _stockCardImageReadRepository = stockCardImageReadRepository;
        }

        public async Task<CreatedStockCardImageResponse> Handle(CreateStockCardImageCommand request, CancellationToken cancellationToken)
        {
            List<StockCardImage> stockCardImages = await _stockCardImageReadRepository.GetAll().Where(x => x.GidStockCardFK == request.GidStockCardFK).ToListAsync();
            int maxRowNo = 0;
            if (stockCardImages.Count > 0)
            {
                maxRowNo = stockCardImages.Max(r => r.RowNo);
            }


            StockCardImage stockCardImage = _mapper.Map<StockCardImage>(request);
            stockCardImage.RowNo = maxRowNo + 1;

            await _stockCardImageWriteRepository.AddAsync(stockCardImage);
            await _stockCardImageWriteRepository.SaveAsync();

            StockCardImage savedStockCardImage = await _stockCardImageReadRepository.GetAsync(predicate: x => x.Gid == stockCardImage.Gid, include: i => i.Include(x => x.StockCardFK));


            GetByGidStockCardImageResponse obj = _mapper.Map<GetByGidStockCardImageResponse>(savedStockCardImage);
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