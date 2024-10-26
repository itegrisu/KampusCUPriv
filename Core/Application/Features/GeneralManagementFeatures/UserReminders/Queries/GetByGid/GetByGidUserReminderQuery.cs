using AutoMapper;
using MediatR;
using X = Domain.Entities.GeneralManagements;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using Application.Features.GeneralManagementFeatures.UserReminders.Rules;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByGid
{
    public class GetByGidUserReminderQuery : IRequest<GetByGidUserReminderResponse>
    {
        public Guid Gid { get; set; }

        public class GetByGidUserReminderQueryHandler : IRequestHandler<GetByGidUserReminderQuery, GetByGidUserReminderResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserReminderReadRepository _userReminderReadRepository;
            private readonly UserReminderBusinessRules _userReminderBusinessRules;

            public GetByGidUserReminderQueryHandler(IMapper mapper, IUserReminderReadRepository userReminderReadRepository, UserReminderBusinessRules userReminderBusinessRules)
            {
                _mapper = mapper;
                _userReminderReadRepository = userReminderReadRepository;
                _userReminderBusinessRules = userReminderBusinessRules;
            }

            public async Task<GetByGidUserReminderResponse> Handle(GetByGidUserReminderQuery request, CancellationToken cancellationToken)
            {
                X.UserReminder? userReminder = await _userReminderReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK));
                    //unutma
					//includes varsa eklenecek - Orn: Altta
					//include: i => i.Include(i => i.AcademicTitleFK).Include(i => i.UniversityFK)
                await _userReminderBusinessRules.UserReminderShouldExistWhenSelected(userReminder);

                GetByGidUserReminderResponse response = _mapper.Map<GetByGidUserReminderResponse>(userReminder);
                return response;
            }
        }
    }
}