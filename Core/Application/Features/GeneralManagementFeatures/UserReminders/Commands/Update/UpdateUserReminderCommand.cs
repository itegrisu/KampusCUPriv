using Application.Features.GeneralManagementFeatures.UserReminders.Constants;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserReminders.Rules;
using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.Update;

public class UpdateUserReminderCommand : IRequest<UpdatedUserReminderResponse>
{
    public Guid Gid { get; set; }

	public Guid GidUserFK { get; set; }

public DateTime Date { get; set; }
public string Title { get; set; }
public string? Description { get; set; }
public string? Document { get; set; }
public EnumReminderType ReminderType { get; set; }



    public class UpdateUserReminderCommandHandler : IRequestHandler<UpdateUserReminderCommand, UpdatedUserReminderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserReminderWriteRepository _userReminderWriteRepository;
        private readonly IUserReminderReadRepository _userReminderReadRepository;
        private readonly UserReminderBusinessRules _userReminderBusinessRules;

        public UpdateUserReminderCommandHandler(IMapper mapper, IUserReminderWriteRepository userReminderWriteRepository,
                                         UserReminderBusinessRules userReminderBusinessRules, IUserReminderReadRepository userReminderReadRepository)
        {
            _mapper = mapper;
            _userReminderWriteRepository = userReminderWriteRepository;
            _userReminderBusinessRules = userReminderBusinessRules;
            _userReminderReadRepository = userReminderReadRepository;
        }

        public async Task<UpdatedUserReminderResponse> Handle(UpdateUserReminderCommand request, CancellationToken cancellationToken)
        {
            X.UserReminder? userReminder = await _userReminderReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
            await _userReminderBusinessRules.UserReminderShouldExistWhenSelected(userReminder);
            userReminder = _mapper.Map(request, userReminder);

            _userReminderWriteRepository.Update(userReminder!);
            await _userReminderWriteRepository.SaveAsync();
            GetByGidUserReminderResponse obj = _mapper.Map<GetByGidUserReminderResponse>(userReminder);

            return new()
            {
                Title = UserRemindersBusinessMessages.ProcessCompleted,
                Message = UserRemindersBusinessMessages.SuccessCreatedUserReminderMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}