using Application.Helpers.PaginationHelpers;
using Application.Repositories.PersonnelManagementRepos.PersonnelGraduatedSchoolRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.PersonnelManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.PersonnelManagements;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Queries.GetList;

public class GetListPersonnelGraduatedSchoolQuery : IRequest<GetListResponse<GetListPersonnelGraduatedSchoolListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListPersonnelGraduatedSchoolQueryHandler : IRequestHandler<GetListPersonnelGraduatedSchoolQuery, GetListResponse<GetListPersonnelGraduatedSchoolListItemDto>>
    {
        private readonly IPersonnelGraduatedSchoolReadRepository _personnelGraduatedSchoolReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.PersonnelGraduatedSchool, GetListPersonnelGraduatedSchoolListItemDto> _noPagination;

        public GetListPersonnelGraduatedSchoolQueryHandler(IPersonnelGraduatedSchoolReadRepository personnelGraduatedSchoolReadRepository, IMapper mapper, NoPagination<X.PersonnelGraduatedSchool, GetListPersonnelGraduatedSchoolListItemDto> noPagination)
        {
            _personnelGraduatedSchoolReadRepository = personnelGraduatedSchoolReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListPersonnelGraduatedSchoolListItemDto>> Handle(GetListPersonnelGraduatedSchoolQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
            {
                return await _noPagination.NoPaginationData(cancellationToken,
                    includes: new Expression<Func<PersonnelGraduatedSchool, object>>[]
                    {
                       x => x.UserFK,
                    });
            }

            IPaginate<X.PersonnelGraduatedSchool> personnelGraduatedSchools = await _personnelGraduatedSchoolReadRepository.GetListAllAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK)
            );

            GetListResponse<GetListPersonnelGraduatedSchoolListItemDto> response = _mapper.Map<GetListResponse<GetListPersonnelGraduatedSchoolListItemDto>>(personnelGraduatedSchools);
            return response;
        }
    }
}