using Application.Features.SupportManagementFeatures.SupportMessageDetails.Constants;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportMessageDetails.Rules;
using Application.Repositories.SupportManagementRepos.SupportMessageDetailRepo;
using AutoMapper;
using X = Domain.Entities.SupportManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Helpers;
using Domain.Entities.SupportManagements;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Application.Features.SupportManagementFeatures.SupportMessageDetails.Commands.Create;

public class CreateSupportMessageDetailCommand : IRequest<CreatedSupportMessageDetailResponse>
{
    public Guid GidSupportFK { get; set; }
    public Guid GidReadUserFK { get; set; }
 

    public class CreateSupportMessageDetailCommandHandler : IRequestHandler<CreateSupportMessageDetailCommand, CreatedSupportMessageDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportMessageDetailWriteRepository _supportMessageDetailWriteRepository;
        private readonly ISupportMessageDetailReadRepository _supportMessageDetailReadRepository;
        private readonly ISupportMessageReadRepository _supportMessageReadRepository;
        private readonly ISupportRequestReadRepository _supportRequestReadRepository;
        private readonly SupportMessageDetailBusinessRules _supportMessageDetailBusinessRules;
        private readonly GetUserInfo _getUserInfo;


        public CreateSupportMessageDetailCommandHandler(IMapper mapper, ISupportMessageDetailWriteRepository supportMessageDetailWriteRepository,
                                         SupportMessageDetailBusinessRules supportMessageDetailBusinessRules, ISupportMessageDetailReadRepository supportMessageDetailReadRepository, GetUserInfo getUserInfo, ISupportMessageReadRepository supportMessageReadRepository)
        {
            _mapper = mapper;
            _supportMessageDetailWriteRepository = supportMessageDetailWriteRepository;
            _supportMessageDetailBusinessRules = supportMessageDetailBusinessRules;
            _supportMessageDetailReadRepository = supportMessageDetailReadRepository;
            _getUserInfo = getUserInfo;
            _supportMessageReadRepository = supportMessageReadRepository;
        }

        public async Task<CreatedSupportMessageDetailResponse> Handle(CreateSupportMessageDetailCommand request, CancellationToken cancellationToken)
        {
            await _supportMessageDetailBusinessRules.UserShouldExistWhenSelected(request.GidReadUserFK);
            await _supportMessageDetailBusinessRules.SupportRequestShouldExistWhenSelected(request.GidSupportFK);

            var supportMessages = await _supportMessageReadRepository.GetListAsync(
               predicate: x => x.GidSupportFK == request.GidSupportFK,
               index: 0,
               size:1000,
               include: x=>x.Include(x=>x.SupportMessageDetails)
               );



            var unreadSupportMessages = supportMessages.Items
                .Where(sm => !sm.SupportMessageDetails.Any(smd => smd.GidReadUserFK == request.GidReadUserFK))
                .ToList();


            //var notReadMessages = supportMessages.Items
            //                             .Where(x => x.SupportMessageDetails == null)
            //                                   .ToList();
            List<SupportMessageDetail> supportMessageDetails = new List<SupportMessageDetail>();
            SupportMessageDetail supportMessageDetail = new();

         
            foreach (var message in unreadSupportMessages)
            {
                supportMessageDetail.GidMessageFK = message.Gid;
                supportMessageDetail.GidReadUserFK = request.GidReadUserFK;
                supportMessageDetail.ReadDate = DateTime.Now;
                supportMessageDetail.ReadIp = _getUserInfo.GetUserIpAddress();
                supportMessageDetails.Add(supportMessageDetail);

            }
            await _supportMessageDetailWriteRepository.AddRangeAsync(supportMessageDetails);
            await _supportMessageDetailWriteRepository.SaveAsync();



            return new()
            {
                Title = SupportMessageDetailsBusinessMessages.ProcessCompleted,
                Message = SupportMessageDetailsBusinessMessages.SuccessCreatedSupportMessageDetailMessage,
                IsValid = true,
                //Obj = obj
            };

           
        }
    }
}