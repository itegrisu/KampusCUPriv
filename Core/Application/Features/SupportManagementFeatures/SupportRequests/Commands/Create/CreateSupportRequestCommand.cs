using Application.Abstractions.UnitOfWork;
using Application.Features.SupportManagementFeatures.SupportRequests.Constants;
using Application.Features.SupportManagementFeatures.SupportRequests.Queries.GetByGid;
using Application.Features.SupportManagementFeatures.SupportRequests.Rules;
using Application.Repositories.SupportManagementRepos.SupportMessageRepo;
using Application.Repositories.SupportManagementRepos.SupportRequestRepo;
using AutoMapper;
using Core.CrossCuttingConcern.Exceptions;
using Domain.Entities.SupportManagements;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupportManagements;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Commands.Create;

public class CreateSupportRequestCommand : IRequest<CreatedSupportRequestResponse>
{
    public Guid CreatedUserFK { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public EnumSupportStatus SupportStatus { get; set; }
    public EnumPriorityType PriorityType { get; set; }
    public EnumSupportType SupportType { get; set; }



    public class CreateSupportRequestCommandHandler : IRequestHandler<CreateSupportRequestCommand, CreatedSupportRequestResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISupportRequestWriteRepository _supportRequestWriteRepository;
        private readonly ISupportRequestReadRepository _supportRequestReadRepository;
        private readonly ISupportMessageWriteRepository _supportMessageWriteRepository;
        private readonly SupportRequestBusinessRules _supportRequestBusinessRules;
        private readonly IUnitOfWork _unitOfWork;
        public CreateSupportRequestCommandHandler(IMapper mapper, ISupportRequestWriteRepository supportRequestWriteRepository,
                                         SupportRequestBusinessRules supportRequestBusinessRules, ISupportRequestReadRepository supportRequestReadRepository, ISupportMessageWriteRepository supportMessageWriteRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _supportRequestWriteRepository = supportRequestWriteRepository;
            _supportRequestBusinessRules = supportRequestBusinessRules;
            _supportRequestReadRepository = supportRequestReadRepository;
            _supportMessageWriteRepository = supportMessageWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreatedSupportRequestResponse> Handle(CreateSupportRequestCommand request, CancellationToken cancellationToken)
        {
            X.SupportRequest supportRequest = _mapper.Map<X.SupportRequest>(request);
            request.SupportStatus = EnumSupportStatus.SupportRequestCreated;

            _unitOfWork.BeginTransaction();
            try
            {
                // Add the support request and save it
                await _supportRequestWriteRepository.AddAsync(supportRequest);
                await _unitOfWork.SaveChangesAsync();

                // Add the support message
                await _supportMessageWriteRepository.AddAsync(new SupportMessage
                {
                    MessageType = EnumMessageType.Message,
                    GidSupportFK = supportRequest.Gid,
                    Message = request.Message,
                    GidSenderUserFK = request.CreatedUserFK,
                });
                await _unitOfWork.SaveChangesAsync();

                // Commit the transaction
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                throw new BusinessException("An error was encountered while creating a Support Request");
            }
            //TODO: Buraya bak
            //finally
            //{
            //    await _unitOfWork.RollbackAsync();
            //}


            X.SupportRequest? savedSupportRequest = await _supportRequestReadRepository.GetAsync(predicate: x => x.Gid == supportRequest.Gid,
                include: x => x.Include(i => i.UserFK));


            GetByGidSupportRequestResponse obj = _mapper.Map<GetByGidSupportRequestResponse>(savedSupportRequest);
            return new()
            {
                Title = SupportRequestsBusinessMessages.ProcessCompleted,
                Message = SupportRequestsBusinessMessages.SuccessCreatedSupportRequestMessage,
                IsValid = true,
                Obj = obj
            };
        }

    }
}