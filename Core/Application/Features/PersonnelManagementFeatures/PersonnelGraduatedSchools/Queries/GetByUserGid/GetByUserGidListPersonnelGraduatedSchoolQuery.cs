using Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetList;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelGraduatedSchoolQuery : IRequest<GetListResponse<GetByUserGidListPersonnelGraduatedSchoolListItemDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Guid UserGid { get; set; }
        public class GetByUserGidListPersonnelGraduatedSchoolQueryHandler : IRequestHandler<GetByUserGidListPersonnelGraduatedSchoolQuery, GetListResponse<GetByUserGidListPersonnelGraduatedSchoolListItemDto>>
        {
            private readonly IPersonnelGraduatedSchoolReadRepository _personnelGraduatedSchoolReadRepository;
            private readonly IMapper _mapper;
            private readonly NoPagination<X.PersonnelGraduatedSchool, GetByUserGidListPersonnelGraduatedSchoolListItemDto> _noPagination;

            public GetByUserGidListPersonnelGraduatedSchoolQueryHandler(IPersonnelGraduatedSchoolReadRepository personnelGraduatedSchoolReadRepository, IMapper mapper, NoPagination<X.PersonnelGraduatedSchool, GetByUserGidListPersonnelGraduatedSchoolListItemDto> noPagination)
            {
                _personnelGraduatedSchoolReadRepository = personnelGraduatedSchoolReadRepository;
                _mapper = mapper;
                _noPagination = noPagination;
            }

            public async Task<GetListResponse<GetByUserGidListPersonnelGraduatedSchoolListItemDto>> Handle(GetByUserGidListPersonnelGraduatedSchoolQuery request, CancellationToken cancellationToken)
            {
                if (request.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationData(cancellationToken,
                        predicate: x => x.GidPersonnelFK == request.UserGid,
                        includes: new Expression<Func<PersonnelGraduatedSchool, object>>[]
                        {
                       x => x.UserFK,
                        });
                }

                IPaginate<X.PersonnelGraduatedSchool> personnelGraduatedSchools = await _personnelGraduatedSchoolReadRepository.GetListAllAsync(
                    index: request.PageIndex,
                    size: request.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.GidPersonnelFK == request.UserGid,
                    include: x => x.Include(x => x.UserFK)
                );

                GetListResponse<GetByUserGidListPersonnelGraduatedSchoolListItemDto> response = _mapper.Map<GetListResponse<GetByUserGidListPersonnelGraduatedSchoolListItemDto>>(personnelGraduatedSchools);
                return response;
            }
        }
    }
}
