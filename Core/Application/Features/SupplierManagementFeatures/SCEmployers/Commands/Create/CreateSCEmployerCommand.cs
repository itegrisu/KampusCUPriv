using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Rules;
using Application.Repositories.SupplierManagementRepos.SCEmployerRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Create;

public class CreateSCEmployerCommand : IRequest<CreatedSCEmployerResponse>
{
    public Guid GidSCCompanyFK { get; set; }
    public string FullName { get; set; }
    public string Duty { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? SpecialNote { get; set; }



    public class CreateSCEmployerCommandHandler : IRequestHandler<CreateSCEmployerCommand, CreatedSCEmployerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCEmployerWriteRepository _sCEmployerWriteRepository;
        private readonly ISCEmployerReadRepository _sCEmployerReadRepository;
        private readonly SCEmployerBusinessRules _sCEmployerBusinessRules;

        public CreateSCEmployerCommandHandler(IMapper mapper, ISCEmployerWriteRepository sCEmployerWriteRepository,
                                         SCEmployerBusinessRules sCEmployerBusinessRules, ISCEmployerReadRepository sCEmployerReadRepository)
        {
            _mapper = mapper;
            _sCEmployerWriteRepository = sCEmployerWriteRepository;
            _sCEmployerBusinessRules = sCEmployerBusinessRules;
            _sCEmployerReadRepository = sCEmployerReadRepository;
        }

        public async Task<CreatedSCEmployerResponse> Handle(CreateSCEmployerCommand request, CancellationToken cancellationToken)
        {
            await _sCEmployerBusinessRules.SCCompanyShouldExistWhenSelected(request.GidSCCompanyFK);

            X.SCEmployer sCEmployer = _mapper.Map<X.SCEmployer>(request);
            //sCEmployer.RowNo = maxRowNo + 1;

            await _sCEmployerWriteRepository.AddAsync(sCEmployer);
            await _sCEmployerWriteRepository.SaveAsync();

            X.SCEmployer savedSCEmployer = await _sCEmployerReadRepository.GetAsync(predicate: x => x.Gid == sCEmployer.Gid, include: x => x.Include(x => x.SCCompanyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidSCEmployerResponse obj = _mapper.Map<GetByGidSCEmployerResponse>(savedSCEmployer);
            return new()
            {
                Title = SCEmployersBusinessMessages.ProcessCompleted,
                Message = SCEmployersBusinessMessages.SuccessCreatedSCEmployerMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}