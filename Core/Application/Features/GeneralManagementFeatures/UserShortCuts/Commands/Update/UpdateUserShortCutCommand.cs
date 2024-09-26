using Application.Features.GeneralManagementFeatures.UserShortCuts.Constants;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Queries.GetByGid;
using Application.Features.GeneralManagementFeatures.UserShortCuts.Rules;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Update;

public class UpdateUserShortCutCommand : IRequest<UpdatedUserShortCutResponse>
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string PageName { get; set; }
    public string PageUrl { get; set; }
    public int RowNo { get; set; }



    public class UpdateUserShortCutCommandHandler : IRequestHandler<UpdateUserShortCutCommand, UpdatedUserShortCutResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserShortCutWriteRepository _userShortCutWriteRepository;
        private readonly IUserShortCutReadRepository _userShortCutReadRepository;
        private readonly UserShortCutBusinessRules _userShortCutBusinessRules;

        public UpdateUserShortCutCommandHandler(IMapper mapper, IUserShortCutWriteRepository userShortCutWriteRepository,
                                         UserShortCutBusinessRules userShortCutBusinessRules, IUserShortCutReadRepository userShortCutReadRepository)
        {
            _mapper = mapper;
            _userShortCutWriteRepository = userShortCutWriteRepository;
            _userShortCutBusinessRules = userShortCutBusinessRules;
            _userShortCutReadRepository = userShortCutReadRepository;
        }

        public async Task<UpdatedUserShortCutResponse> Handle(UpdateUserShortCutCommand request, CancellationToken cancellationToken)
        {
            UserShortCut? userShortCut = await _userShortCutReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _userShortCutBusinessRules.UserShortCutShouldExistWhenSelected(userShortCut);
            await _userShortCutBusinessRules.UserShouldExistWhenSelected(request.GidUserFK);
            userShortCut = _mapper.Map(request, userShortCut);

            _userShortCutWriteRepository.Update(userShortCut!);
            await _userShortCutWriteRepository.SaveAsync();
            GetByGidUserShortCutResponse obj = _mapper.Map<GetByGidUserShortCutResponse>(userShortCut);

            return new()
            {
                Title = UserShortCutsBusinessMessages.ProcessCompleted,
                Message = UserShortCutsBusinessMessages.SuccessUpdatedUserShortCutMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}