using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Constants;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetByGid;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Rules;
using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using AutoMapper;
using X = Domain.Entities.LogManagements;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Create;

public class CreateLogAuthorizationErrorCommand : IRequest<CreatedLogAuthorizationErrorResponse>
{
    public Guid GidUserFK { get; set; }
  public string? IpAddress { get; set; }
  public string PageInfo { get; set; }
 public string? Operation { get; set; }
 public string? JSonData { get; set; }



    public class CreateLogAuthorizationErrorCommandHandler : IRequestHandler<CreateLogAuthorizationErrorCommand, CreatedLogAuthorizationErrorResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogAuthorizationErrorWriteRepository _logAuthorizationErrorWriteRepository;
        private readonly ILogAuthorizationErrorReadRepository _logAuthorizationErrorReadRepository;
        private readonly LogAuthorizationErrorBusinessRules _logAuthorizationErrorBusinessRules;

        public CreateLogAuthorizationErrorCommandHandler(IMapper mapper, ILogAuthorizationErrorWriteRepository logAuthorizationErrorWriteRepository,
                                         LogAuthorizationErrorBusinessRules logAuthorizationErrorBusinessRules, ILogAuthorizationErrorReadRepository logAuthorizationErrorReadRepository)
        {
            _mapper = mapper;
            _logAuthorizationErrorWriteRepository = logAuthorizationErrorWriteRepository;
            _logAuthorizationErrorBusinessRules = logAuthorizationErrorBusinessRules;
            _logAuthorizationErrorReadRepository = logAuthorizationErrorReadRepository;
        }

        public async Task<CreatedLogAuthorizationErrorResponse> Handle(CreateLogAuthorizationErrorCommand request, CancellationToken cancellationToken)
        {
            //int maxRowNo = await _logAuthorizationErrorReadRepository.GetAll().MaxAsync(r => r.RowNo);
			X.LogAuthorizationError logAuthorizationError = _mapper.Map<X.LogAuthorizationError>(request);
            //logAuthorizationError.RowNo = maxRowNo + 1;

            await _logAuthorizationErrorWriteRepository.AddAsync(logAuthorizationError);
            await _logAuthorizationErrorWriteRepository.SaveAsync();

			X.LogAuthorizationError savedLogAuthorizationError = await _logAuthorizationErrorReadRepository.GetAsync(predicate: x => x.Gid == logAuthorizationError.Gid);
			//INCLUDES Buraya Gelecek include varsa eklenecek
			//include: x => x.Include(x => x.UserFK));

            GetByGidLogAuthorizationErrorResponse obj = _mapper.Map<GetByGidLogAuthorizationErrorResponse>(savedLogAuthorizationError);
            return new()
            {           
                Title = LogAuthorizationErrorsBusinessMessages.ProcessCompleted,
                Message = LogAuthorizationErrorsBusinessMessages.SuccessCreatedLogAuthorizationErrorMessage,
                IsValid = true,
                Obj = obj
            };
        }
    }
}