using Application.Features.VehicleManagementFeatures.VehicleDocuments.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.VehicleManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.VehicleManagements;

namespace Application.Features.VehicleManagementsFeatures.VehicleDocuments.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleDocumentQuery : IRequest<GetListResponse<GetByVehicleGidListVehicleDocumentListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid GidVehicleFK { get; set; }
        public class GetByVehicleGidListVehicleDocumentQueryHandler : IRequestHandler<GetByVehicleGidListVehicleDocumentQuery, GetListResponse<GetByVehicleGidListVehicleDocumentListItemDto>>
        {
            private readonly IVehicleDocumentReadRepository _vehicleDocumentReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.VehicleDocument, GetByVehicleGidListVehicleDocumentListItemDto> _noPagination;

            public GetByVehicleGidListVehicleDocumentQueryHandler(IVehicleDocumentReadRepository vehicleDocumentReadRepository, IMapper mapper, NoPagination<X.VehicleDocument, GetByVehicleGidListVehicleDocumentListItemDto> noPagination)
            {
                _vehicleDocumentReadRepository = vehicleDocumentReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByVehicleGidListVehicleDocumentListItemDto>> Handle(GetByVehicleGidListVehicleDocumentQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                    //unutma
                    //includes varsa eklenecek - Orn: Altta
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidVehicleFK == request.GidVehicleFK,
                        includes: new Expression<Func<VehicleDocument, object>>[]
                        {
                       x => x.VehicleAllFK,
                       x=> x.DocumentTypeFK
                        });
                //return await _noPagination.NoPaginationData(cancellationToken);
                IPaginate<X.VehicleDocument> vehicleDocuments = await _vehicleDocumentReadRepository.GetListAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    include: x => x.Include(x => x.VehicleAllFK).Include(x => x.DocumentTypeFK),
                    predicate: x => x.GidVehicleFK == request.GidVehicleFK
                );

                GetListResponse<GetByVehicleGidListVehicleDocumentListItemDto> response = _mapper.Map<GetListResponse<GetByVehicleGidListVehicleDocumentListItemDto>>(vehicleDocuments);
                return response;
            }
        }
    }
}
