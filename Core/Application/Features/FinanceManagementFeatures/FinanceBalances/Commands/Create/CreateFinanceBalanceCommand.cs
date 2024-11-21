using Application.Features.FinanceManagementFeatures.FinanceBalances.Constants;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Create;

public class CreateFinanceBalanceCommand : IRequest<CreatedFinanceBalanceResponse>
{
    public Guid GidSupplierCustomerFK { get; set; }
    public Guid GidVehicleTransactionFK { get; set; }
    public Guid GidTransportationFK { get; set; }
    public Guid GidTransportationExternalServiceFK { get; set; }
    public Guid GidFeeCurrencyFK { get; set; }
    public EnumBalanceType BalanceType { get; set; }
    public EnumBalanceResourceType BalanceResourceType { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Fee { get; set; }
    public EnumPaymentStatus PaymentStatus { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentFile { get; set; }
    public string? Description { get; set; }



    public class CreateFinanceBalanceCommandHandler : IRequestHandler<CreateFinanceBalanceCommand, CreatedFinanceBalanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceBalanceWriteRepository _financeBalanceWriteRepository;
        private readonly IFinanceBalanceReadRepository _financeBalanceReadRepository;
        private readonly FinanceBalanceBusinessRules _financeBalanceBusinessRules;

        public CreateFinanceBalanceCommandHandler(IMapper mapper, IFinanceBalanceWriteRepository financeBalanceWriteRepository,
                                         FinanceBalanceBusinessRules financeBalanceBusinessRules, IFinanceBalanceReadRepository financeBalanceReadRepository)
        {
            _mapper = mapper;
            _financeBalanceWriteRepository = financeBalanceWriteRepository;
            _financeBalanceBusinessRules = financeBalanceBusinessRules;
            _financeBalanceReadRepository = financeBalanceReadRepository;
        }

        public async Task<CreatedFinanceBalanceResponse> Handle(CreateFinanceBalanceCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _financeBalanceReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.FinanceBalance financeBalance = _mapper.Map<X.FinanceBalance>(request);
            //financeBalance.RowNo = maxRowNo + 1;

            await _financeBalanceWriteRepository.AddAsync(financeBalance);
            await _financeBalanceWriteRepository.SaveAsync();

            X.FinanceBalance savedFinanceBalance = await _financeBalanceReadRepository.GetAsync(predicate: x => x.Gid == financeBalance.Gid,
                include: x => x.Include(x => x.CurrencyFK).Include(x => x.SCCompanyFK).Include(x => x.TransportationExternalServiceFK).Include(x => x.TransportationFK).Include(x => x.VehicleTransactionFK).Include(x => x.VehicleTransactionFK).ThenInclude(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidFinanceBalanceResponse obj = _mapper.Map<GetByGidFinanceBalanceResponse>(savedFinanceBalance);
            return new()
            {
                Title = FinanceBalancesBusinessMessages.ProcessCompleted,
                Message = FinanceBalancesBusinessMessages.SuccessCreatedFinanceBalanceMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}