using Application.Features.StockManagementFeatures.StockCardImages.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.StockManagementRepos.StockCardImageRepo;
using AutoMapper;
using MediatR;
using X = Domain.Entities.StockManagements;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.UpdateRowNo
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
            private readonly UpdateRowNoHelper<UpdateRowNoStockCardImageResponse, X.StockCardImage> _updateRowNoHelper;

            public UpdateRowNoStockCardImageCommandHandler(IMapper mapper, IStockCardImageWriteRepository stockCardImageWriteRepository,
                                             StockCardImageBusinessRules stockCardImageBusinessRules, IStockCardImageReadRepository stockCardImageReadRepository, UpdateRowNoHelper<UpdateRowNoStockCardImageResponse, X.StockCardImage> updateRowNoHelper)
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
                List<X.StockCardImage> lst = _stockCardImageReadRepository.GetAll().Where(x => x.GidStockCardFK == stockCardImage.GidStockCardFK).OrderBy(a => a.RowNo).ToList();

                X.StockCardImage select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoStockCardImageResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _stockCardImageWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}