using Application.Features.GeneralManagementFeatures.UserModuleAuths.Constants;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserModuleAuths.Rules;
using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.GeneralManagements;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Create;

public class CreateUserModuleAuthCommand : IRequest<CreatedUserModuleAuthResponse>
{
    public Guid GidUserFK { get; set; }
    public EnumModuleType ModuleType { get; set; }

    public class CreateUserModuleAuthCommandHandler : IRequestHandler<CreateUserModuleAuthCommand, CreatedUserModuleAuthResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserModuleAuthWriteRepository _userModuleAuthWriteRepository;
        private readonly IUserModuleAuthReadRepository _userModuleAuthReadRepository;
        private readonly UserModuleAuthBusinessRules _userModuleAuthBusinessRules;

        public CreateUserModuleAuthCommandHandler(IMapper mapper, IUserModuleAuthWriteRepository userModuleAuthWriteRepository,
                                         UserModuleAuthBusinessRules userModuleAuthBusinessRules, IUserModuleAuthReadRepository userModuleAuthReadRepository)
        {
            _mapper = mapper;
            _userModuleAuthWriteRepository = userModuleAuthWriteRepository;
            _userModuleAuthBusinessRules = userModuleAuthBusinessRules;
            _userModuleAuthReadRepository = userModuleAuthReadRepository;
        }

        public async Task<CreatedUserModuleAuthResponse> Handle(CreateUserModuleAuthCommand request, CancellationToken cancellationToken)
        {
            await _userModuleAuthBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            X.UserModuleAuth userModuleAuth = _mapper.Map<X.UserModuleAuth>(request);


            await _userModuleAuthWriteRepository.AddAsync(userModuleAuth);
            await _userModuleAuthWriteRepository.SaveAsync();

            X.UserModuleAuth savedUserModuleAuth = await _userModuleAuthReadRepository.GetAsync(predicate: x => x.Gid == userModuleAuth.Gid, include: x => x.Include(x => x.UserFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidUserModuleAuthResponse obj = _mapper.Map<GetByGidUserModuleAuthResponse>(savedUserModuleAuth);
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