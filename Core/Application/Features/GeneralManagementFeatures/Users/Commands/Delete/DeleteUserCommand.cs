using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Features.GeneralManagementFeatures.Users.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.Users.Rules;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<DeletedUserResponse>
{
    //public int Gid { get; set; }
    public Guid Gid { get; set; }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly UserBusinessRules _userCustomBusinessRules;

        public DeleteUserCommandHandler(IMapper mapper, IUserReadRepository userReadRepository,
                                         UserBusinessRules userCustomBusinessRules, IUserWriteRepository userWriteRepository)
        {
            _mapper = mapper;
            _userReadRepository = userReadRepository;
            _userCustomBusinessRules = userCustomBusinessRules;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userCustomBusinessRules.UserCustomIdShouldExistWhenSelected(request.Gid);
            User? user = await _userReadRepository.GetAsync(predicate: uc => uc.Gid == request.Gid, cancellationToken: cancellationToken);

            user.DataState = Core.Enum.DataState.Deleted;
            _userWriteRepository.Update(user);
            await _userWriteRepository.SaveAsync();

            var deletedUser = await _userReadRepository.GetListAllAsync(predicate: u => u.Gid == request.Gid,
                include: u => u.Include(u => u.CountryFK));

            GetByGidUserResponse obj = _mapper.Map<GetByGidUserResponse>(deletedUser.Items[0]);

            return new()
            {
                Obj = obj,
                Title = UsersBusinessMessages.ProcessCompleted,
                Message = UsersBusinessMessages.SuccessDeletedUserMessage,
                IsValid = true
            };
        }
    }
}