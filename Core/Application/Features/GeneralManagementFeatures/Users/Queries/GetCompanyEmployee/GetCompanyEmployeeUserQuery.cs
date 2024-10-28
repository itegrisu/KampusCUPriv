using Application.Features.GeneralManagementFeatures.Users.Queries.GetSystemAdmin;
using Application.Helpers.PaginationHelpers;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Queries.GetCompanyEmployee
{
    public class GetCompanyEmployeeUserQuery : IRequest<GetListResponse<GetCompanyEmployeeUserListItemDto>>
    {
        public PageRequest PageRequest { get; set; }
        public class GetCompanyEmployeeUserQueryHandler : IRequestHandler<GetCompanyEmployeeUserQuery, GetListResponse<GetCompanyEmployeeUserListItemDto>>
        {
            private readonly NoPagination<User, GetCompanyEmployeeUserListItemDto> _noPagination;
            private readonly IUserReadRepository _userReadRepository;
            private readonly IMapper _mapper;

            public GetCompanyEmployeeUserQueryHandler(NoPagination<User, GetCompanyEmployeeUserListItemDto> noPagination, IUserReadRepository userReadRepository, IMapper mapper)
            {
                _noPagination = noPagination;
                _userReadRepository = userReadRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetCompanyEmployeeUserListItemDto>> Handle(GetCompanyEmployeeUserQuery request, CancellationToken cancellationToken)
            {
                if (request.PageRequest.PageIndex == -1)
                {
                    return await _noPagination.NoPaginationAllData(
                        cancellationToken,
                        predicate:x => x.WorkType == EnumWorkType.FirmaCalisani,
                        includes: new Expression<Func<User, object>>[]
                        {
                         x => x.CountryFK,
                        });
                }

                IPaginate<User> userCustoms = await _userReadRepository.GetListAllAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken,
                    predicate: x => x.WorkType == EnumWorkType.FirmaCalisani

                );

                GetListResponse<GetCompanyEmployeeUserListItemDto> response = _mapper.Map<GetListResponse<GetCompanyEmployeeUserListItemDto>>(userCustoms);
                return response;
            }
        }
    }

}
