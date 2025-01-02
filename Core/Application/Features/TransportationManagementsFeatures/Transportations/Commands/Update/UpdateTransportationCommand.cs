using Application.Features.TransportationManagementFeatures.Transportations.Constants;
using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.Transportations.Rules;
using Application.Repositories.TransportationRepos.TransportationRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.Transportations.Commands.Update;

public class UpdateTransportationCommand : IRequest<UpdatedTransportationResponse>
{
    public Guid Gid { get; set; }
    public Guid GidOrganizationFK { get; set; }
    public Guid GidFeeCurrencyFK { get; set; }
    public string CustomerInfo { get; set; }
    public string TransportationNo { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Fee { get; set; }
    public EnumTransportationStatus TransportationStatus { get; set; }



    public class UpdateTransportationCommandHandler : IRequestHandler<UpdateTransportationCommand, UpdatedTransportationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationWriteRepository _transportationWriteRepository;
        private readonly ITransportationReadRepository _transportationReadRepository;
        private readonly TransportationBusinessRules _transportationBusinessRules;

        public UpdateTransportationCommandHandler(IMapper mapper, ITransportationWriteRepository transportationWriteRepository,
                                         TransportationBusinessRules transportationBusinessRules, ITransportationReadRepository transportationReadRepository)
        {
            _mapper = mapper;
            _transportationWriteRepository = transportationWriteRepository;
            _transportationBusinessRules = transportationBusinessRules;
            _transportationReadRepository = transportationReadRepository;
        }

        public async Task<UpdatedTransportationResponse> Handle(UpdateTransportationCommand request, CancellationToken cancellationToken)
        {
            X.Transportation? transportation = await _transportationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken,
                   include: x => x.Include(x => x.OrganizationFK).Include(x => x.FeeCurrencyFK));
                   //include: x => x.Include(x => x.OrganizationFK).Include(x => x.CurrencyFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            await _transportationBusinessRules.TransportationShouldExistWhenSelected(transportation);
            transportation = _mapper.Map(request, transportation);

            _transportationWriteRepository.Update(transportation!);
            await _transportationWriteRepository.SaveAsync();

            X.Transportation? transportationUpdated = await _transportationReadRepository.GetAsync(predicate: x => x.Gid == request.Gid, cancellationToken: cancellationToken,
                 include: x => x.Include(x => x.OrganizationFK).Include(x => x.FeeCurrencyFK));
            GetByGidTransportationResponse obj = _mapper.Map<GetByGidTransportationResponse>(transportationUpdated);

            return new()
            {
                Title = TransportationsBusinessMessages.ProcessCompleted,
                Message = TransportationsBusinessMessages.SuccessCreatedTransportationMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}