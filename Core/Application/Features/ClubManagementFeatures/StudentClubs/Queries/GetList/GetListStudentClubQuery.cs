using Application.Helpers.PaginationHelpers;
using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.ClubManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X = Domain.Entities.ClubManagements;

namespace Application.Features.ClubFeatures.StudentClubs.Queries.GetList;

public class GetListStudentClubQuery : IRequest<GetListResponse<GetListStudentClubListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListStudentClubQueryHandler : IRequestHandler<GetListStudentClubQuery, GetListResponse<GetListStudentClubListItemDto>>
    {
        private readonly IStudentClubReadRepository _studentClubReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<X.StudentClub, GetListStudentClubListItemDto> _noPagination;

        public GetListStudentClubQueryHandler(IStudentClubReadRepository studentClubReadRepository, IMapper mapper, NoPagination<X.StudentClub, GetListStudentClubListItemDto> noPagination)
        {
            _studentClubReadRepository = studentClubReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListStudentClubListItemDto>> Handle(GetListStudentClubQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken,
                    orderBy: x => x.ClubFK.Name,
                    includes: new Expression<Func<StudentClub, object>>[]
                    {
                       x => x.UserFK,
                       x => x.ClubFK
                    });
            IPaginate<X.StudentClub> studentClubs = await _studentClubReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: x => x.Include(x => x.UserFK).Include(x => x.ClubFK),
                orderBy: x => x.OrderBy(x => x.ClubFK.Name)
            );

            GetListResponse<GetListStudentClubListItemDto> response = _mapper.Map<GetListResponse<GetListStudentClubListItemDto>>(studentClubs);
            return response;
        }
    }
}