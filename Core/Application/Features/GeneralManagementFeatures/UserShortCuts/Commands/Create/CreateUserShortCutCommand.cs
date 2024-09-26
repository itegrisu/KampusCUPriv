using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using Domain.Entities.GeneralManagements;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Constants;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Rules;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Create;

public class CreateUserShortCutCommand : IRequest<CreatedUserShortCutResponse>
{
    public Guid GidUserFK { get; set; }
    public string PageName { get; set; }
    public string PageUrl { get; set; }



    public class CreateUserShortCutCommandHandler : IRequestHandler<CreateUserShortCutCommand, CreatedUserShortCutResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserShortCutWriteRepository _userShortCutWriteRepository;
        private readonly IUserShortCutReadRepository _userShortCutReadRepository;
        private readonly UserShortCutBusinessRules _userShortCutBusinessRules;

        public CreateUserShortCutCommandHandler(IMapper mapper, IUserShortCutWriteRepository userShortCutWriteRepository,
                                         UserShortCutBusinessRules userShortCutBusinessRules, IUserShortCutReadRepository userShortCutReadRepository)
        {
            _mapper = mapper;
            _userShortCutWriteRepository = userShortCutWriteRepository;
            _userShortCutBusinessRules = userShortCutBusinessRules;
            _userShortCutReadRepository = userShortCutReadRepository;
        }

        public async Task<CreatedUserShortCutResponse> Handle(CreateUserShortCutCommand request, CancellationToken cancellationToken)
        {
            await _userShortCutBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            int maxRowNo = 0;
            if (_userShortCutReadRepository.Count() > 0)
            {
                maxRowNo = await _userShortCutReadRepository.GetAll().MaxAsync(r => r.RowNo);
            }

            UserShortCut userShortCut = _mapper.Map<UserShortCut>(request);

            userShortCut.RowNo = maxRowNo + 1;

            await _userShortCutWriteRepository.AddAsync(userShortCut);
            await _userShortCutWriteRepository.SaveAsync();

            UserShortCut savedUserShortCut = await _userShortCutReadRepository.GetAsync(predicate: x => x.Gid == userShortCut.Gid);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidUserShortCutResponse obj = _mapper.Map<GetByGidUserShortCutResponse>(savedUserShortCut);
            return new()
            {
                Title = UserShortCutsBusinessMessages.ProcessCompleted,
                Message = UserShortCutsBusinessMessages.SuccessCreatedUserShortCutMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}