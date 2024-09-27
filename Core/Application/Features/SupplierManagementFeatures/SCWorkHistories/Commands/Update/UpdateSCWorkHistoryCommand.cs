using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Rules;
using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Update;

public class UpdateSCWorkHistoryCommand : IRequest<UpdatedSCWorkHistoryResponse>
{
    public Guid Gid { get; set; }

    public Guid GidSCCompanyFK { get; set; }

    public string Title { get; set; }
    public string? Detail { get; set; }
    public DateTime? WorkDate { get; set; }
    public string? WorkFile { get; set; }



    public class UpdateSCWorkHistoryCommandHandler : IRequestHandler<UpdateSCWorkHistoryCommand, UpdatedSCWorkHistoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCWorkHistoryWriteRepository _sCWorkHistoryWriteRepository;
        private readonly ISCWorkHistoryReadRepository _sCWorkHistoryReadRepository;
        private readonly SCWorkHistoryBusinessRules _sCWorkHistoryBusinessRules;

        public UpdateSCWorkHistoryCommandHandler(IMapper mapper, ISCWorkHistoryWriteRepository sCWorkHistoryWriteRepository,
                                         SCWorkHistoryBusinessRules sCWorkHistoryBusinessRules, ISCWorkHistoryReadRepository sCWorkHistoryReadRepository)
        {
            _mapper = mapper;
            _sCWorkHistoryWriteRepository = sCWorkHistoryWriteRepository;
            _sCWorkHistoryBusinessRules = sCWorkHistoryBusinessRules;
            _sCWorkHistoryReadRepository = sCWorkHistoryReadRepository;
        }

        public async Task<UpdatedSCWorkHistoryResponse> Handle(UpdateSCWorkHistoryCommand request, CancellationToken cancellationToken)
        {
            X.SCWorkHistory? sCWorkHistory = await _sCWorkHistoryReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _sCWorkHistoryBusinessRules.SCWorkHistoryShouldExistWhenSelected(sCWorkHistory);
            await _sCWorkHistoryBusinessRules.SCCompanyShouldExistWhenSelected(request.GidSCCompanyFK);

            sCWorkHistory = _mapper.Map(request, sCWorkHistory);

            _sCWorkHistoryWriteRepository.Update(sCWorkHistory!);
            await _sCWorkHistoryWriteRepository.SaveAsync();

            X.SCWorkHistory updatedSCWorkHistory = await _sCWorkHistoryReadRepository.GetAsync(predicate: x => x.Gid == sCWorkHistory.Gid, include: x => x.Include(x => x.SCCompanyFK));

            GetByGidSCWorkHistoryResponse obj = _mapper.Map<GetByGidSCWorkHistoryResponse>(updatedSCWorkHistory);

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