using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Rules;
using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Create;

public class CreateSCWorkHistoryCommand : IRequest<CreatedSCWorkHistoryResponse>
{
    public Guid GidSCCompanyFK { get; set; }
    public string Title { get; set; }
    public string? Detail { get; set; }
    public DateTime? WorkDate { get; set; }
    public string? WorkFile { get; set; }



    public class CreateSCWorkHistoryCommandHandler : IRequestHandler<CreateSCWorkHistoryCommand, CreatedSCWorkHistoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCWorkHistoryWriteRepository _sCWorkHistoryWriteRepository;
        private readonly ISCWorkHistoryReadRepository _sCWorkHistoryReadRepository;
        private readonly SCWorkHistoryBusinessRules _sCWorkHistoryBusinessRules;

        public CreateSCWorkHistoryCommandHandler(IMapper mapper, ISCWorkHistoryWriteRepository sCWorkHistoryWriteRepository,
                                         SCWorkHistoryBusinessRules sCWorkHistoryBusinessRules, ISCWorkHistoryReadRepository sCWorkHistoryReadRepository)
        {
            _mapper = mapper;
            _sCWorkHistoryWriteRepository = sCWorkHistoryWriteRepository;
            _sCWorkHistoryBusinessRules = sCWorkHistoryBusinessRules;
            _sCWorkHistoryReadRepository = sCWorkHistoryReadRepository;
        }

        public async Task<CreatedSCWorkHistoryResponse> Handle(CreateSCWorkHistoryCommand request, CancellationToken cancellationToken)
        {

            X.SCWorkHistory sCWorkHistory = _mapper.Map<X.SCWorkHistory>(request);

            await _sCWorkHistoryWriteRepository.AddAsync(sCWorkHistory);
            await _sCWorkHistoryWriteRepository.SaveAsync();

            X.SCWorkHistory savedSCWorkHistory = await _sCWorkHistoryReadRepository.GetAsync(predicate: x => x.Gid == sCWorkHistory.Gid, include: x => x.Include(x => x.SCCompanyFK));


            GetByGidSCWorkHistoryResponse obj = _mapper.Map<GetByGidSCWorkHistoryResponse>(savedSCWorkHistory);
            return new()
            {
                Title = SCWorkHistoriesBusinessMessages.ProcessCompleted,
                Message = SCWorkHistoriesBusinessMessages.SuccessCreatedSCWorkHistoryMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}