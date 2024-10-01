using Application.Features.WareHouseManagementFeatures.StockCardImages.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.WarehouseManagementRepos.StockCardImageRepo;
using AutoMapper;
using Domain.Entities.WarehouseManagements;
using MediatR;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.UpdateRowNo
{
    public class UpdateRowNoStockCardImageCommand : IRequest<UpdateRowNoStockCardImageResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoStockCardImageCommandHandler : IRequestHandler<UpdateRowNoStockCardImageCommand, UpdateRowNoStockCardImageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStockCardImageWriteRepository _stockCardImageWriteRepository;
            private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
            private readonly StockCardImageBusinessRules _stockCardImageBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoStockCardImageResponse, StockCardImage> _updateRowNoHelper;

            public UpdateRowNoStockCardImageCommandHandler(IMapper mapper, IStockCardImageWriteRepository stockCardImageWriteRepository,
                                             StockCardImageBusinessRules stockCardImageBusinessRules, IStockCardImageReadRepository stockCardImageReadRepository, UpdateRowNoHelper<UpdateRowNoStockCardImageResponse, StockCardImage> updateRowNoHelper)
            {
                _mapper = mapper;
                _stockCardImageWriteRepository = stockCardImageWriteRepository;
                _stockCardImageBusinessRules = stockCardImageBusinessRules;
                _stockCardImageReadRepository = stockCardImageReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoStockCardImageResponse> Handle(UpdateRowNoStockCardImageCommand request, CancellationToken cancellationToken)
            {
                var stockCardImage = await _stockCardImageReadRepository.GetAsync(x => x.Gid == request.Gid);
                List<StockCardImage> lst = _stockCardImageReadRepository.GetAll().Where(x => x.GidStockCardFK == stockCardImage.GidStockCardFK).OrderBy(a => a.RowNo).ToList();

                StockCardImage select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoStockCardImageResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _stockCardImageWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}