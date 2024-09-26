using Application.Abstractions.Storage;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Domain.Entities.GeneralManagements;
using Domain.Entities.SupportManagements;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using Application.Features.SupportManagementFeatures.SupportMessages.Rules;
using Domain.Enums;
using Application.Features.SupportManagementFeatures.SupportMessages.Constants;
using Application.Features.SupportManagementFeatures.SupportMessages.Queries.GetByGid;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.UploadFile
{
    public class UploadSupportFileCommand : IRequest<UploadSupportFileResponse>
    {
        public Guid SupportRequestGid { get; set; }
        public Guid SenderUserGid { get; set; }
        public string FileName { get; set; }

        public class UploadSupportFileCommandHandler : IRequestHandler<UploadSupportFileCommand, UploadSupportFileResponse>
        {
            private readonly SupportMessageBusinessRules _supportFileBusinessRules;
            private readonly ISupportMessageWriteRepository _supportMessageWriteRepository;
            private readonly IFileTypeCheckService _fileTypeCheckService;
            private readonly IStorageService _storageService;
            private readonly ISupportMessageReadRepository _supportMessageReadRepository;
            private readonly IMapper _mapper;

            public UploadSupportFileCommandHandler(IStorageService storageService, IFileTypeCheckService fileTypeCheckService, ISupportMessageWriteRepository supportMessageWriteRepository, SupportMessageBusinessRules supportFileBusinessRules, ISupportMessageReadRepository supportMessageReadRepository, IMapper mapper)
            {
                _storageService = storageService;
                _fileTypeCheckService = fileTypeCheckService;
                _supportMessageWriteRepository = supportMessageWriteRepository;
                _supportFileBusinessRules = supportFileBusinessRules;
                _supportMessageReadRepository = supportMessageReadRepository;
                _mapper = mapper;
            }

            public async Task<UploadSupportFileResponse> Handle(UploadSupportFileCommand request, CancellationToken cancellationToken)
            {
                await _supportFileBusinessRules.SupportRequestShouldExistWhenSelected(request.SupportRequestGid);
                await _supportFileBusinessRules.UserShouldExistWhenSelected(request.SenderUserGid);


                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/support-message-file");
                string extension = Path.GetExtension(request.FileName);

                EnumMessageType messageType;
                if (extension == ".pdf")
                    messageType = EnumMessageType.PDFFile;
                else
                    messageType = EnumMessageType.Image;

                SupportMessage supportMessage = new SupportMessage
                {
                    Message = "\\Files\\support-message-file\\" + request.FileName,
                    MessageType = messageType,
                    GidSenderUserFK = request.SenderUserGid,
                    GidSupportFK = request.SupportRequestGid
                };

                await _supportMessageWriteRepository.AddAsync(supportMessage);
                await _supportMessageWriteRepository.SaveAsync();

                SupportMessage responseSupportMessage = await _supportMessageReadRepository.GetSingleAsync(x => x.Gid == supportMessage.Gid);
                GetByGidSupportMessageResponse obj = _mapper.Map<GetByGidSupportMessageResponse>(responseSupportMessage);

                return new()
                {
                    Title = SupportMessagesBusinessMessages.ProcessCompleted,
                    Message = SupportMessagesBusinessMessages.SucessUploadMessage,
                    IsValid = true,
                    Obj = obj
                };
            }


        }
    }
}