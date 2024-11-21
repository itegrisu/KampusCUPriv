using Application.Features.TransportationManagementFeatures.Transportations.Constants;
using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid;
using Application.Features.TransportationManagementFeatures.Transportations.Rules;
using Application.Repositories.TransportationRepos.TransportationRepo;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using X = Domain.Entities.TransportationManagements;

namespace Application.Features.TransportationManagementFeatures.Transportations.Commands.Create;

public class CreateTransportationCommand : IRequest<CreatedTransportationResponse>
{
    public Guid GidOrganizationFK { get; set; }
    public string CustomerInfo { get; set; }
    public string TransportationNo { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Fee { get; set; }
    public EnumTransportationStatus TransportationStatus { get; set; }



    public class CreateTransportationCommandHandler : IRequestHandler<CreateTransportationCommand, CreatedTransportationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransportationWriteRepository _transportationWriteRepository;
        private readonly ITransportationReadRepository _transportationReadRepository;
        private readonly TransportationBusinessRules _transportationBusinessRules;

        public CreateTransportationCommandHandler(IMapper mapper, ITransportationWriteRepository transportationWriteRepository,
                                         TransportationBusinessRules transportationBusinessRules, ITransportationReadRepository transportationReadRepository)
        {
            _mapper = mapper;
            _transportationWriteRepository = transportationWriteRepository;
            _transportationBusinessRules = transportationBusinessRules;
            _transportationReadRepository = transportationReadRepository;
        }

        public async Task<CreatedTransportationResponse> Handle(CreateTransportationCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _transportationReadRepository.GetAll().MaxAsync(r => r.RowNo);
            X.Transportation transportation = _mapper.Map<X.Transportation>(request);
            //transportation.RowNo = maxRowNo + 1;

            await _transportationWriteRepository.AddAsync(transportation);
            await _transportationWriteRepository.SaveAsync();

            X.Transportation savedTransportation = await _transportationReadRepository.GetAsync(predicate: x => x.Gid == transportation.Gid,
                include: x => x.Include(x => x.OrganizationFK));
            //INCLUDES Buraya Gelecek include varsa eklenecek
            //include: x => x.Include(x => x.UserFK));

            GetByGidTransportationResponse obj = _mapper.Map<GetByGidTransportationResponse>(savedTransportation);
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