using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralFeatures.Users.Rules;
using AutoMapper;
using X = Domain.Entities.GeneralManagements;
using MediatR;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GeneralFeatures.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>
{
    public Guid Gid { get; set; }
    public Guid GidDepartmentFK { get; set; }
    public Guid GidClassFK { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? IsBloodDonor { get; set; }
    public bool IsEmailVerified { get; set; }
    public string? EmailVerificationCode { get; set; }
    public DateTime? EmailVerificationCodeExpire { get; set; }
    public string? DeviceToken { get; set; }
    public bool IsNotificationsEnabled { get; set; }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public UpdateUserCommandHandler(IMapper mapper, IUserWriteRepository userWriteRepository,
                                         UserBusinessRules userBusinessRules, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _userWriteRepository = userWriteRepository;
            _userBusinessRules = userBusinessRules;
            _userReadRepository = userReadRepository;
        }

        public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            X.User? user = await _userReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.ClassFK).Include(x => x.DepartmentFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _userBusinessRules.UserShouldExistWhenSelected(user);
            user = _mapper.Map(request, user);

            _userWriteRepository.Update(user!);
            await _userWriteRepository.SaveAsync();
            GetByGidUserResponse obj = _mapper.Map<GetByGidUserResponse>(user);

            return new()
            {
                Title = UsersBusinessMessages.ProcessCompleted,
                Message = UsersBusinessMessages.SuccessCreatedUserMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}