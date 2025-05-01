using Application.Features.GeneralFeatures.Admins.Constants;
using Application.Features.GeneralFeatures.Admins.Rules;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using Application.Repositories.GeneralManagementRepo.AdminRepo;

namespace Application.Features.GeneralFeatures.Admins.Commands.Delete;

public class DeleteAdminCommand : IRequest<DeletedAdminResponse>
{
	public Guid Gid { get; set; }

    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, DeletedAdminResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdminReadRepository _adminReadRepository;
        private readonly IAdminWriteRepository _adminWriteRepository;
        private readonly AdminBusinessRules _adminBusinessRules;

        public DeleteAdminCommandHandler(IMapper mapper, IAdminReadRepository adminReadRepository,
                                         AdminBusinessRules adminBusinessRules, IAdminWriteRepository adminWriteRepository)
        {
            _mapper = mapper;
            _adminReadRepository = adminReadRepository;
            _adminBusinessRules = adminBusinessRules;
            _adminWriteRepository = adminWriteRepository;
        }

        public async Task<DeletedAdminResponse> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            X.Admin? admin = await _adminReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _adminBusinessRules.AdminShouldExistWhenSelected(admin);
            admin.DataState = Core.Enum.DataState.Deleted;

            _adminWriteRepository.Update(admin);
            await _adminWriteRepository.SaveAsync();

            return new()
            {
                Title = AdminsBusinessMessages.ProcessCompleted,
                Message = AdminsBusinessMessages.SuccessDeletedAdminMessage,
                IsValid = true
            };
        }
    }
}