using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Rules;
using Application.Repositories.SupplierManagementRepos.SCEmployerRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Update;

public class UpdateSCEmployerCommand : IRequest<UpdatedSCEmployerResponse>
{
    public Guid Gid { get; set; }
    public Guid GidSCCompanyFK { get; set; }
    public string FullName { get; set; }
    public string Duty { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? SpecialNote { get; set; }



    public class UpdateSCEmployerCommandHandler : IRequestHandler<UpdateSCEmployerCommand, UpdatedSCEmployerResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCEmployerWriteRepository _sCEmployerWriteRepository;
        private readonly ISCEmployerReadRepository _sCEmployerReadRepository;
        private readonly SCEmployerBusinessRules _sCEmployerBusinessRules;

        public UpdateSCEmployerCommandHandler(IMapper mapper, ISCEmployerWriteRepository sCEmployerWriteRepository,
                                         SCEmployerBusinessRules sCEmployerBusinessRules, ISCEmployerReadRepository sCEmployerReadRepository)
        {
            _mapper = mapper;
            _sCEmployerWriteRepository = sCEmployerWriteRepository;
            _sCEmployerBusinessRules = sCEmployerBusinessRules;
            _sCEmployerReadRepository = sCEmployerReadRepository;
        }

        public async Task<UpdatedSCEmployerResponse> Handle(UpdateSCEmployerCommand request, CancellationToken cancellationToken)
        {
            X.SCEmployer? sCEmployer = await _sCEmployerReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _sCEmployerBusinessRules.SCEmployerShouldExistWhenSelected(sCEmployer);
            await _sCEmployerBusinessRules.SCCompanyShouldExistWhenSelected(request.GidSCCompanyFK);
            sCEmployer = _mapper.Map(request, sCEmployer);

            _sCEmployerWriteRepository.Update(sCEmployer!);
            await _sCEmployerWriteRepository.SaveAsync();
            X.SCEmployer savedSCEmployer = await _sCEmployerReadRepository.GetAsync(predicate: x => x.Gid == sCEmployer.Gid, include: x => x.Include(x => x.SCCompanyFK));
            GetByGidSCEmployerResponse obj = _mapper.Map<GetByGidSCEmployerResponse>(sCEmployer);

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