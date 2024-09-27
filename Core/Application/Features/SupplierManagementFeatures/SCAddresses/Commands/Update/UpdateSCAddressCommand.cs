using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Constants;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetByGid;
using Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Rules;
using Application.Repositories.SupplierManagementRepos.SCAddressRepo;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.SupplierCustomerManagements;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Update;

public class UpdateSCAddressCommand : IRequest<UpdatedSCAddressResponse>
{
    public Guid Gid { get; set; }

    public Guid GidSCCompanyFK { get; set; }
    public Guid GidCityFK { get; set; }

    public string Title { get; set; }
    public string? District { get; set; }
    public string? PostalCode { get; set; }
    public string Address { get; set; }



    public class UpdateSCAddressCommandHandler : IRequestHandler<UpdateSCAddressCommand, UpdatedSCAddressResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISCAddressWriteRepository _sCAddressWriteRepository;
        private readonly ISCAddressReadRepository _sCAddressReadRepository;
        private readonly SCAddressBusinessRules _sCAddressBusinessRules;

        public UpdateSCAddressCommandHandler(IMapper mapper, ISCAddressWriteRepository sCAddressWriteRepository,
                                         SCAddressBusinessRules sCAddressBusinessRules, ISCAddressReadRepository sCAddressReadRepository)
        {
            _mapper = mapper;
            _sCAddressWriteRepository = sCAddressWriteRepository;
            _sCAddressBusinessRules = sCAddressBusinessRules;
            _sCAddressReadRepository = sCAddressReadRepository;
        }

        public async Task<UpdatedSCAddressResponse> Handle(UpdateSCAddressCommand request, CancellationToken cancellationToken)
        {
            X.SCAddress? sCAddress = await _sCAddressReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken);
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _sCAddressBusinessRules.SCAddressShouldExistWhenSelected(sCAddress);
            await _sCAddressBusinessRules.SCCompanyShouldExistWhenSelected(request.GidSCCompanyFK);
            await _sCAddressBusinessRules.CityShouldExistWhenSelected(request.GidCityFK);
            sCAddress = _mapper.Map(request, sCAddress);

            _sCAddressWriteRepository.Update(sCAddress!);
            await _sCAddressWriteRepository.SaveAsync();
            X.SCAddress savedSCAddress = await _sCAddressReadRepository.GetAsync(predicate: x => x.Gid == sCAddress.Gid, include: x => x.Include(x => x.SCCompanyFK).Include(x => x.CityFK));

            GetByGidSCAddressResponse obj = _mapper.Map<GetByGidSCAddressResponse>(sCAddress);

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