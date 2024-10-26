using Application.Features.GeneralManagementFeatures.UserReminders.Constants;
using Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserReminders.Rules;
using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.Create;

public class CreateUserReminderCommand : IRequest<CreatedUserReminderResponse>
{
    public Guid GidUserFK { get; set; }

public DateTime Date { get; set; }
public string Title { get; set; }
public string? Description { get; set; }
public string? Document { get; set; }
public EnumReminderType ReminderType { get; set; }



    public class CreateUserReminderCommandHandler : IRequestHandler<CreateUserReminderCommand, CreatedUserReminderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserReminderWriteRepository _userReminderWriteRepository;
        private readonly IUserReminderReadRepository _userReminderReadRepository;
        private readonly UserReminderBusinessRules _userReminderBusinessRules;

        public CreateUserReminderCommandHandler(IMapper mapper, IUserReminderWriteRepository userReminderWriteRepository,
                                         UserReminderBusinessRules userReminderBusinessRules, IUserReminderReadRepository userReminderReadRepository)
        {
            _mapper = mapper;
            _userReminderWriteRepository = userReminderWriteRepository;
            _userReminderBusinessRules = userReminderBusinessRules;
            _userReminderReadRepository = userReminderReadRepository;
        }

        public async Task<CreatedUserReminderResponse> Handle(CreateUserReminderCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _userReminderReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.UserReminder userReminder = _mapper.Map<X.UserReminder>(request);
            //userReminder.RowNo = maxRowNo + 1;

            await _userReminderWriteRepository.AddAsync(userReminder);
            await _userReminderWriteRepository.SaveAsync();

			X.UserReminder savedUserReminder = await _userReminderReadRepository.GetAsync(predicate: x => x.Gid == userReminder.Gid, include: x => x.Include(x => x.UserFK));
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidUserReminderResponse obj = _mapper.Map<GetByGidUserReminderResponse>(savedUserReminder);
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