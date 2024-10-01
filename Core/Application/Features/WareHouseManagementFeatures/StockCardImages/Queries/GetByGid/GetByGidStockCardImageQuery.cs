using Application.Features.WareHouseManagementFeatures.StockCardImages.Rules;
using Application.Repositories.WarehouseManagementRepos.StockCardImageRepo;
using AutoMapper;
using Domain.Entities.WarehouseManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Queries.GetByGid
{
    public class GetByGidStockCardImageQuery : IRequest<GetByGidStockCardImageResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidStockCardImageQueryHandler : IRequestHandler<GetByGidStockCardImageQuery, GetByGidStockCardImageResponse>
        {
            private readonly IMapper _mapper;
            private readonly IStockCardImageReadRepository _stockCardImageReadRepository;
            private readonly StockCardImageBusinessRules _stockCardImageBusinessRules;

            public GetByGidStockCardImageQueryHandler(IMapper mapper, IStockCardImageReadRepository stockCardImageReadRepository, StockCardImageBusinessRules stockCardImageBusinessRules)
            {
                _mapper = mapper;
                _stockCardImageReadRepository = stockCardImageReadRepository;
                _stockCardImageBusinessRules = stockCardImageBusinessRules;
            }

            public async Task<GetByGidStockCardImageResponse> Handle(GetByGidStockCardImageQuery request, CancellationToken cancellationToken)
            {
                StockCardImage? stockCardImage = await _stockCardImageReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.StockCardFK));

                await _stockCardImageBusinessRules.StockCardImageShouldExistWhenSelected(stockCardImage);

                GetByGidStockCardImageResponse response = _mapper.Map<GetByGidStockCardImageResponse>(stockCardImage);
                return response;
            }
        }
    }
}