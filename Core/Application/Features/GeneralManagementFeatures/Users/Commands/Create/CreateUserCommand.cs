using Application.Features.GeneralFeatures.Users.Constants;
using Application.Features.GeneralFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralFeatures.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>
{
    public Guid GidDepartmentFK { get; set; }
    public Guid GidClassFK { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? IsBloodDonor { get; set; }



    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly UserBusinessRules _userBusinessRules;

        public CreateUserCommandHandler(IMapper mapper, IUserWriteRepository userWriteRepository,
                                         UserBusinessRules userBusinessRules, IUserReadRepository userReadRepository)
        {
            _mapper = mapper;
            _userWriteRepository = userWriteRepository;
            _userBusinessRules = userBusinessRules;
            _userReadRepository = userReadRepository;
        }

        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _userReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.User user = _mapper.Map<X.User>(request);
            //user.RowNo = maxRowNo + 1;

            await _userWriteRepository.AddAsync(user);
            await _userWriteRepository.SaveAsync();

            X.User savedUser = await _userReadRepository.GetAsync(predicate: x => x.Gid == user.Gid, include: x => x.Include(x => x.ClassFK).Include(x => x.DepartmentFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidUserResponse obj = _mapper.Map<GetByGidUserResponse>(savedUser);
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