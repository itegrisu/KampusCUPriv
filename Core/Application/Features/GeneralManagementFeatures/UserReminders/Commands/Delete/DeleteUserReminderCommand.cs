using Application.Features.GeneralManagementFeatures.UserReminders.Constants;
using Application.Features.GeneralManagementFeatures.UserReminders.Rules;
using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.Delete;

public class DeleteUserReminderCommand : IRequest<DeletedUserReminderResponse>
{
	public Guid Gid { get; set; }

    public class DeleteUserReminderCommandHandler : IRequestHandler<DeleteUserReminderCommand, DeletedUserReminderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserReminderReadRepository _userReminderReadRepository;
        private readonly IUserReminderWriteRepository _userReminderWriteRepository;
        private readonly UserReminderBusinessRules _userReminderBusinessRules;

        public DeleteUserReminderCommandHandler(IMapper mapper, IUserReminderReadRepository userReminderReadRepository,
                                         UserReminderBusinessRules userReminderBusinessRules, IUserReminderWriteRepository userReminderWriteRepository)
        {
            _mapper = mapper;
            _userReminderReadRepository = userReminderReadRepository;
            _userReminderBusinessRules = userReminderBusinessRules;
            _userReminderWriteRepository = userReminderWriteRepository;
        }

        public async Task<DeletedUserReminderResponse> Handle(DeleteUserReminderCommand request, CancellationToken cancellationToken)
        {
            X.UserReminder? userReminder = await _userReminderReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            await _userReminderBusinessRules.UserReminderShouldExistWhenSelected(userReminder);
            userReminder.DataState = Core.Enum.DataState.Deleted;

            _userReminderWriteRepository.Update(userReminder);
            await _userReminderWriteRepository.SaveAsync();

            return new()
            {
                Title = UserRemindersBusinessMessages.ProcessCompleted,
                Message = UserRemindersBusinessMessages.SuccessDeletedUserReminderMessage,
                IsValid = true
            };
        }
    }
}