using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Rules;
using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Update;

public class UpdateSCPersonnelCommand : IRequest<UpdatedSCPersonnelResponse>
{
    public Guid Gid { get; set; }

    public Guid GidSCCompanyFK { get; set; }
    public Guid GidPersonnelFK { get; set; }
    public EnumSCPersonnelLoginStatus SCPersonnelLoginStatus { get; set; }

    public class UpdateSCPersonnelCommandHandler : IRequestHandler<UpdateSCPersonnelCommand, UpdatedSCPersonnelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCPersonnelWriteRepository _sCPersonnelWriteRepository;
        private readonly ISCPersonnelReadRepository _sCPersonnelReadRepository;
        private readonly SCPersonnelBusinessRules _sCPersonnelBusinessRules;

        public UpdateSCPersonnelCommandHandler(IMapper mapper, ISCPersonnelWriteRepository sCPersonnelWriteRepository,
                                         SCPersonnelBusinessRules sCPersonnelBusinessRules, ISCPersonnelReadRepository sCPersonnelReadRepository)
        {
            _mapper = mapper;
            _sCPersonnelWriteRepository = sCPersonnelWriteRepository;
            _sCPersonnelBusinessRules = sCPersonnelBusinessRules;
            _sCPersonnelReadRepository = sCPersonnelReadRepository;
        }

        public async Task<UpdatedSCPersonnelResponse> Handle(UpdateSCPersonnelCommand request, CancellationToken cancellationToken)
        {
            X.SCPersonnel? sCPersonnel = await _sCPersonnelReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken, include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _sCPersonnelBusinessRules.SCPersonnelShouldExistWhenSelected(sCPersonnel);
            sCPersonnel = _mapper.Map(request, sCPersonnel);

            _sCPersonnelWriteRepository.Update(sCPersonnel!);
            await _sCPersonnelWriteRepository.SaveAsync();
            GetByGidSCPersonnelResponse obj = _mapper.Map<GetByGidSCPersonnelResponse>(sCPersonnel);

            return new()
            {
                Title = SCPersonnelsBusinessMessages.ProcessCompleted,
                Message = SCPersonnelsBusinessMessages.SuccessCreatedSCPersonnelMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}