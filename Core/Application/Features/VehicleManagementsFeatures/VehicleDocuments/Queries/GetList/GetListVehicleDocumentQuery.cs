using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetList;

public class GetListVehicleDocumentQuery : IRequest<GetListResponse<GetListVehicleDocumentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListVehicleDocumentQueryHandler : IRequestHandler<GetListVehicleDocumentQuery, GetListResponse<GetListVehicleDocumentListItemDto>>
    {
        private readonly IVehicleDocumentReadRepository _vehicleDocumentReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.VehicleDocument, GetListVehicleDocumentListItemDto> _noPagination;

        public GetListVehicleDocumentQueryHandler(IVehicleDocumentReadRepository vehicleDocumentReadRepository, IMapper mapper, NoPagination<X.VehicleDocument, GetListVehicleDocumentListItemDto> noPagination)
        {
            _vehicleDocumentReadRepository = vehicleDocumentReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListVehicleDocumentListItemDto>> Handle(GetListVehicleDocumentQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                //unutma
                //includes varsa eklenecek - Orn: Altta
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<VehicleDocument, object>>[]
                    {
                       x => x.VehicleAllFK,
                       x=> x.DocumentTypeFK
                    });
            //return await _noPagination.NoPaginationData(cancellationToken);
            IPaginate<X.VehicleDocument> vehicleDocuments = await _vehicleDocumentReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.VehicleAllFK).Include(x => x.DocumentTypeFK)
            );

            GetListResponse<GetListVehicleDocumentListItemDto> response = _mapper.Map<GetListResponse<GetListVehicleDocumentListItemDto>>(vehicleDocuments);
            return response;
        }
    }
}