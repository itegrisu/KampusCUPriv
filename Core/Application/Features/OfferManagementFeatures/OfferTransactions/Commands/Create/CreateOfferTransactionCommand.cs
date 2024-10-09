using Application.Features.OfferManagementFeatures.OfferTransactions.Constants;
using Application.Features.OfferManagementFeatures.OfferTransactions.Queries.GetByGid;
using Application.Features.OfferManagementFeatures.OfferTransactions.Rules;
using Application.Repositories.OfferManagementRepos.OfferTransactionRepo;
using AutoMapper;
using Domain.Entities.OfferManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.OfferManagements;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Create;

public class CreateOfferTransactionCommand : IRequest<CreatedOfferTransactionResponse>
{
    public Guid GidOfferFK { get; set; }
    public Guid GidCurrencyFK { get; set; }
    public decimal Total { get; set; }
    public DateTime? OfferDeadline { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }



    public class CreateOfferTransactionCommandHandler : IRequestHandler<CreateOfferTransactionCommand, CreatedOfferTransactionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOfferTransactionWriteRepository _offerTransactionWriteRepository;
        private readonly IOfferTransactionReadRepository _offerTransactionReadRepository;
        private readonly OfferTransactionBusinessRules _offerTransactionBusinessRules;

        public CreateOfferTransactionCommandHandler(IMapper mapper, IOfferTransactionWriteRepository offerTransactionWriteRepository,
                                         OfferTransactionBusinessRules offerTransactionBusinessRules, IOfferTransactionReadRepository offerTransactionReadRepository)
        {
            _mapper = mapper;
            _offerTransactionWriteRepository = offerTransactionWriteRepository;
            _offerTransactionBusinessRules = offerTransactionBusinessRules;
            _offerTransactionReadRepository = offerTransactionReadRepository;
        }

        public async Task<CreatedOfferTransactionResponse> Handle(CreateOfferTransactionCommand request, CancellationToken cancellationToken)
        {
            await _offerTransactionBusinessRules.OfferShouldExistWhenSelected(request.GidOfferFK);
            await _offerTransactionBusinessRules.CurrencyShouldExistWhenSelected(request.GidCurrencyFK);



            X.OfferTransaction offerTransaction = _mapper.Map<X.OfferTransaction>(request);

            List<OfferTransaction> requests = _offerTransactionReadRepository.GetAll().OrderByDescending(x => x.CreatedDate).ToList();

            string number = GenerateDocumentNumber(requests);
            offerTransaction.OfferId = number;

            await _offerTransactionWriteRepository.AddAsync(offerTransaction);
            await _offerTransactionWriteRepository.SaveAsync();

            X.OfferTransaction savedOfferTransaction = await _offerTransactionReadRepository.GetAsync(predicate: x => x.Gid == offerTransaction.Gid, include: x => x.Include(x => x.CurrencyFK).Include(x => x.OfferFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidOfferTransactionResponse obj = _mapper.Map<GetByGidOfferTransactionResponse>(savedOfferTransaction);
            return new()
            {
                Title = OfferTransactionsBusinessMessages.ProcessCompleted,
                Message = OfferTransactionsBusinessMessages.SuccessCreatedOfferTransactionMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }

    static string GenerateDocumentNumber(List<OfferTransaction> requests)
    {
        int currentYear = DateTime.Now.Year;
        string prefix = "T" + currentYear.ToString().Substring(2) + "- ";
        int maxSuffix = 0;

        foreach (var request in requests)
        {
            if (request.OfferId.StartsWith(prefix))
            {
                string suffixString = request.OfferId.Substring(prefix.Length);
                if (int.TryParse(suffixString, out int suffix))
                {
                    if (suffix > maxSuffix)
                    {
                        maxSuffix = suffix;
                    }
                }
            }
        }

        int newSuffix = maxSuffix + 1;
        return prefix + newSuffix.ToString("D4"); // D4 formatý 4 haneli sýfýrlarla doldurulmuþ sayý oluþturur
    }

}