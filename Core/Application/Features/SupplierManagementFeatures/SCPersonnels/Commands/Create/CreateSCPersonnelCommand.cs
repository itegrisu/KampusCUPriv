using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Rules;
using AutoMapper;
using X = Domain.Entities.SupplierCustomerManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Create;

public class CreateSCPersonnelCommand : IRequest<CreatedSCPersonnelResponse>
{
    public Guid GidSCCompanyFK { get; set; }
    public Guid GidPersonnelFK { get; set; }
    public EnumSCPersonnelLoginStatus SCPersonnelLoginStatus { get; set; }

    public class CreateSCPersonnelCommandHandler : IRequestHandler<CreateSCPersonnelCommand, CreatedSCPersonnelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCPersonnelWriteRepository _sCPersonnelWriteRepository;
        private readonly ISCPersonnelReadRepository _sCPersonnelReadRepository;
        private readonly SCPersonnelBusinessRules _sCPersonnelBusinessRules;

        public CreateSCPersonnelCommandHandler(IMapper mapper, ISCPersonnelWriteRepository sCPersonnelWriteRepository,
                                         SCPersonnelBusinessRules sCPersonnelBusinessRules, ISCPersonnelReadRepository sCPersonnelReadRepository)
        {
            _mapper = mapper;
            _sCPersonnelWriteRepository = sCPersonnelWriteRepository;
            _sCPersonnelBusinessRules = sCPersonnelBusinessRules;
            _sCPersonnelReadRepository = sCPersonnelReadRepository;
        }

        public async Task<CreatedSCPersonnelResponse> Handle(CreateSCPersonnelCommand request, CancellationToken cancellationToken)
        {
            await _sCPersonnelBusinessRules.SCPersonnelShouldNotBeDuplicated(request.GidPersonnelFK, request.GidSCCompanyFK);

            X.SCPersonnel sCPersonnel = _mapper.Map<X.SCPersonnel>(request);

            await _sCPersonnelWriteRepository.AddAsync(sCPersonnel);
            await _sCPersonnelWriteRepository.SaveAsync();

            X.SCPersonnel? savedSCPersonnel = await _sCPersonnelReadRepository.GetAsync(
                predicate: x => x.Gid == sCPersonnel.Gid,
                include: x => x.Include(x => x.UserFK).Include(x => x.SCCompanyFK)
            );

            if (savedSCPersonnel == null)
            {
                throw new Exception("SCPersonnel could not be found after saving.");
            }

            GetByGidSCPersonnelResponse obj = _mapper.Map<GetByGidSCPersonnelResponse>(savedSCPersonnel);
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