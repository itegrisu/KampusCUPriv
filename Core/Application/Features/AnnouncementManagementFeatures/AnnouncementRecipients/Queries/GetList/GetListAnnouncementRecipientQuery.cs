using Application.Helpers.PaginationHelpers;
using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities.AnnouncementManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetList;

public class GetListAnnouncementRecipientQuery : IRequest<GetListResponse<GetListAnnouncementRecipientListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListAnnouncementRecipientQueryHandler : IRequestHandler<GetListAnnouncementRecipientQuery, GetListResponse<GetListAnnouncementRecipientListItemDto>>
    {
        private readonly IAnnouncementRecipientReadRepository _announcementRecipientReadRepository;
        private readonly IMapper _mapper;
        private readonly NoPagination<AnnouncementRecipient, GetListAnnouncementRecipientListItemDto> _noPagination;

        public GetListAnnouncementRecipientQueryHandler(IAnnouncementRecipientReadRepository announcementRecipientReadRepository, IMapper mapper, NoPagination<AnnouncementRecipient, GetListAnnouncementRecipientListItemDto> noPagination)
        {
            _announcementRecipientReadRepository = announcementRecipientReadRepository;
            _mapper = mapper;
            _noPagination = noPagination;
        }

        public async Task<GetListResponse<GetListAnnouncementRecipientListItemDto>> Handle(GetListAnnouncementRecipientQuery request, CancellationToken cancellationToken)
        {

            if (request.PageRequest.PageIndex == -1)
                return await _noPagination.NoPaginationData(cancellationToken, includes: x => x.UserFK);


            IPaginate<AnnouncementRecipient> announcementRecipients = await _announcementRecipientReadRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                include: x => x.Include(ar => ar.UserFK),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAnnouncementRecipientListItemDto> response = _mapper.Map<GetListResponse<GetListAnnouncementRecipientListItemDto>>(announcementRecipients);
            return response;
        }
    }
}