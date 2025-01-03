using Application.Features.FinanceManagementFeatures.FinanceBalances.Constants;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Queries.GetByGid;
using Application.Features.FinanceManagementFeatures.FinanceBalances.Rules;
using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using AutoMapper;
using X = Domain.Entities.FinanceManagements;
using MediatR;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Update;

public class UpdateFinanceBalanceCommand : IRequest<UpdatedFinanceBalanceResponse>
{
    public Guid Gid { get; set; }
    public Guid GidSupplierCustomerFK { get; set; }
    public Guid? GidVehicleTransactionFK { get; set; }
    public Guid? GidTransportationFK { get; set; }
    public Guid? GidTransportationExternalServiceFK { get; set; }
    public Guid GidFeeCurrencyFK { get; set; }
    public EnumBalanceType BalanceType { get; set; }
    public EnumBalanceResourceType BalanceResourceType { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Fee { get; set; }
    public EnumPaymentStatus PaymentStatus { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentFile { get; set; }
    public string? Description { get; set; }

    public class UpdateFinanceBalanceCommandHandler : IRequestHandler<UpdateFinanceBalanceCommand, UpdatedFinanceBalanceResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFinanceBalanceWriteRepository _financeBalanceWriteRepository;
        private readonly IFinanceBalanceReadRepository _financeBalanceReadRepository;
        private readonly FinanceBalanceBusinessRules _financeBalanceBusinessRules;

        public UpdateFinanceBalanceCommandHandler(IMapper mapper, IFinanceBalanceWriteRepository financeBalanceWriteRepository,
                                         FinanceBalanceBusinessRules financeBalanceBusinessRules, IFinanceBalanceReadRepository financeBalanceReadRepository)
        {
            _mapper = mapper;
            _financeBalanceWriteRepository = financeBalanceWriteRepository;
            _financeBalanceBusinessRules = financeBalanceBusinessRules;
            _financeBalanceReadRepository = financeBalanceReadRepository;
        }

        public async Task<UpdatedFinanceBalanceResponse> Handle(UpdateFinanceBalanceCommand request, CancellationToken cancellationToken)
        {
            X.FinanceBalance? financeBalance = await _financeBalanceReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken,
                  include: x => x.Include(x => x.CurrencyFK).Include(x => x.SCCompanyFK).Include(x => x.TransportationExternalServiceFK).Include(x => x.TransportationFK).Include(x => x.VehicleTransactionFK).Include(x => x.VehicleTransactionFK).ThenInclude(x => x.VehicleAllFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _financeBalanceBusinessRules.FinanceBalanceShouldExistWhenSelected(financeBalance);
            financeBalance = _mapper.Map(request, financeBalance);

            _financeBalanceWriteRepository.Update(financeBalance!);
            await _financeBalanceWriteRepository.SaveAsync();
            GetByGidFinanceBalanceResponse obj = _mapper.Map<GetByGidFinanceBalanceResponse>(financeBalance);

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