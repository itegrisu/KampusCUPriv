using Application.Features.GeneralManagementFeatures.UserModuleAuths.Constants;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Rules;
using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Update;

public class UpdateUserModuleAuthCommand : IRequest<UpdatedUserModuleAuthResponse>
{
    public Guid Gid { get; set; }

    public Guid GidUserFK { get; set; }

    public EnumModuleType ModuleType { get; set; }



    public class UpdateUserModuleAuthCommandHandler : IRequestHandler<UpdateUserModuleAuthCommand, UpdatedUserModuleAuthResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserModuleAuthWriteRepository _userModuleAuthWriteRepository;
        private readonly IUserModuleAuthReadRepository _userModuleAuthReadRepository;
        private readonly UserModuleAuthBusinessRules _userModuleAuthBusinessRules;

        public UpdateUserModuleAuthCommandHandler(IMapper mapper, IUserModuleAuthWriteRepository userModuleAuthWriteRepository,
                                         UserModuleAuthBusinessRules userModuleAuthBusinessRules, IUserModuleAuthReadRepository userModuleAuthReadRepository)
        {
            _mapper = mapper;
            _userModuleAuthWriteRepository = userModuleAuthWriteRepository;
            _userModuleAuthBusinessRules = userModuleAuthBusinessRules;
            _userModuleAuthReadRepository = userModuleAuthReadRepository;
        }

        public async Task<UpdatedUserModuleAuthResponse> Handle(UpdateUserModuleAuthCommand request, CancellationToken cancellationToken)
        {
            X.UserModuleAuth? userModuleAuth = await _userModuleAuthReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _userModuleAuthBusinessRules.UserModuleAuthShouldExistWhenSelected(userModuleAuth);
            await _userModuleAuthBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            userModuleAuth = _mapper.Map(request, userModuleAuth);

            _userModuleAuthWriteRepository.Update(userModuleAuth!);
            await _userModuleAuthWriteRepository.SaveAsync();

            X.UserModuleAuth updatedUserModuleAuth = await _userModuleAuthReadRepository.GetAsync(predicate: x => x.Gid == userModuleAuth.Gid, include: x => x.Include(x => x.UserFK));


            GetByGidUserModuleAuthResponse obj = _mapper.Map<GetByGidUserModuleAuthResponse>(updatedUserModuleAuth);

            return new()
            {
                Title = UserModuleAuthsBusinessMessages.ProcessCompleted,
                Message = UserModuleAuthsBusinessMessages.SuccessCreatedUserModuleAuthMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}