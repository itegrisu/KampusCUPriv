using Application.Features.GeneralManagementFeatures.UserShortCuts.Rules;
using Application.Helpers.UpdateRowNo;
using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using AutoMapper;
using Domain.Entities.GeneralManagements;
using MediatR;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.UpdateRowNo
{
    public class UpdateRowNoUserShortCutCommand : IRequest<UpdateRowNoUserShortCutResponse>
    {
        public Guid Gid { get; set; }
        public bool IsUp { get; set; }

        public class UpdateRowNoUserShortCutCommandHandler : IRequestHandler<UpdateRowNoUserShortCutCommand, UpdateRowNoUserShortCutResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUserShortCutWriteRepository _userShortCutWriteRepository;
            private readonly IUserShortCutReadRepository _userShortCutReadRepository;
            private readonly UserShortCutBusinessRules _userShortCutBusinessRules;
            private readonly UpdateRowNoHelper<UpdateRowNoUserShortCutResponse, UserShortCut> _updateRowNoHelper;

            public UpdateRowNoUserShortCutCommandHandler(IMapper mapper, IUserShortCutWriteRepository userShortCutWriteRepository,
                                             UserShortCutBusinessRules userShortCutBusinessRules, IUserShortCutReadRepository userShortCutReadRepository, UpdateRowNoHelper<UpdateRowNoUserShortCutResponse, UserShortCut> updateRowNoHelper)
            {
                _mapper = mapper;
                _userShortCutWriteRepository = userShortCutWriteRepository;
                _userShortCutBusinessRules = userShortCutBusinessRules;
                _userShortCutReadRepository = userShortCutReadRepository;
                _updateRowNoHelper = updateRowNoHelper;
            }

            public async Task<UpdateRowNoUserShortCutResponse> Handle(UpdateRowNoUserShortCutCommand request, CancellationToken cancellationToken)
            {
                List<UserShortCut> lst = _userShortCutReadRepository.GetAll().OrderBy(a => a.RowNo).ToList();

                UserShortCut select = lst.Where(a => a.Gid == request.Gid).FirstOrDefault();

                UpdateRowNoUserShortCutResponse response = await _updateRowNoHelper.UpdateRowNo(lst, select, request.IsUp);

                await _userShortCutWriteRepository.SaveAsync();

                return response;
            }
        }
    }
}