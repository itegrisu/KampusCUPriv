using Application.Features.GeneralManagementFeatures.UserShortCuts.Constants;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Rules;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Delete;

public class DeleteUserShortCutCommand : IRequest<DeletedUserShortCutResponse>
{
    public Guid Gid { get; set; }

    public class DeleteUserShortCutCommandHandler : IRequestHandler<DeleteUserShortCutCommand, DeletedUserShortCutResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserShortCutReadRepository _userShortCutReadRepository;
        private readonly IUserShortCutWriteRepository _userShortCutWriteRepository;
        private readonly UserShortCutBusinessRules _userShortCutBusinessRules;

        public DeleteUserShortCutCommandHandler(IMapper mapper, IUserShortCutReadRepository userShortCutReadRepository,
                                         UserShortCutBusinessRules userShortCutBusinessRules, IUserShortCutWriteRepository userShortCutWriteRepository)
        {
            _mapper = mapper;
            _userShortCutReadRepository = userShortCutReadRepository;
            _userShortCutBusinessRules = userShortCutBusinessRules;
            _userShortCutWriteRepository = userShortCutWriteRepository;
        }

        public async Task<DeletedUserShortCutResponse> Handle(DeleteUserShortCutCommand request, CancellationToken cancellationToken)
        {
            UserShortCut? userShortCut = await _userShortCutReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _userShortCutBusinessRules.UserShortCutShouldExistWhenSelected(userShortCut);
            userShortCut.DataState = Core.Enum.DataState.Deleted;

            _userShortCutWriteRepository.Update(userShortCut);
            await _userShortCutWriteRepository.SaveAsync();

            return new()
            {
                Title = UserShortCutsBusinessMessages.ProcessCompleted,
                Message = UserShortCutsBusinessMessages.SuccessDeletedUserShortCutMessage,
                IsValid = true
            };
        }
    }
}