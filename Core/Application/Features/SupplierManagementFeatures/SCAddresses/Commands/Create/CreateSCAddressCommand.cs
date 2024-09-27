using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Rules;
using Application.Repositories.SupplierManagementRepos.SCAddressRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Create;

public class CreateSCAddressCommand : IRequest<CreatedSCAddressResponse>
{
    public Guid GidSCCompanyFK { get; set; }
    public Guid GidCityFK { get; set; }
    public string Title { get; set; }
    public string? District { get; set; }
    public string? PostalCode { get; set; }
    public string Address { get; set; }



    public class CreateSCAddressCommandHandler : IRequestHandler<CreateSCAddressCommand, CreatedSCAddressResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCAddressWriteRepository _sCAddressWriteRepository;
        private readonly ISCAddressReadRepository _sCAddressReadRepository;
        private readonly SCAddressBusinessRules _sCAddressBusinessRules;

        public CreateSCAddressCommandHandler(IMapper mapper, ISCAddressWriteRepository sCAddressWriteRepository,
                                         SCAddressBusinessRules sCAddressBusinessRules, ISCAddressReadRepository sCAddressReadRepository)
        {
            _mapper = mapper;
            _sCAddressWriteRepository = sCAddressWriteRepository;
            _sCAddressBusinessRules = sCAddressBusinessRules;
            _sCAddressReadRepository = sCAddressReadRepository;
        }

        public async Task<CreatedSCAddressResponse> Handle(CreateSCAddressCommand request, CancellationToken cancellationToken)
        {

            await _sCAddressBusinessRules.SCCompanyShouldExistWhenSelected(request.GidSCCompanyFK);
            await _sCAddressBusinessRules.CityShouldExistWhenSelected(request.GidCityFK);

            X.SCAddress sCAddress = _mapper.Map<X.SCAddress>(request);

            await _sCAddressWriteRepository.AddAsync(sCAddress);
            await _sCAddressWriteRepository.SaveAsync();

            X.SCAddress savedSCAddress = await _sCAddressReadRepository.GetAsync(predicate: x => x.Gid == sCAddress.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CityFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidSCAddressResponse obj = _mapper.Map<GetByGidSCAddressResponse>(savedSCAddress);
            return new()
            {
                Title = SCAddressesBusinessMessages.ProcessCompleted,
                Message = SCAddressesBusinessMessages.SuccessCreatedSCAddressMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}