using Application.Abstractions.Storage;
using Application.Features.AnnouncementManagementFeatures.Announcements.Constants;
using Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Rules;
using Application.Features.GeneralManagementFeatures.Users.Constants;
using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using Domain.Entities.FinanceManagements;
using MediatR;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.UploadDocumentFile
{
    public class UploadDetailDocumentCommand : IRequest<UploadDetailDocumentResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }

        public class UploadDetailDocumentCommandHandler : IRequestHandler<UploadDetailDocumentCommand, UploadDetailDocumentResponse>
        {
            private readonly IFinanceExpenseDetailReadRepository _readRepository;
            private readonly IFinanceExpenseDetailWriteRepository _writeRepository;
            private readonly FinanceExpenseDetailBusinessRules _rules;
            private readonly IStorageService _storageService;


            public UploadDetailDocumentCommandHandler(IFinanceExpenseDetailReadRepository readRepository, IFinanceExpenseDetailWriteRepository writeRepository, FinanceExpenseDetailBusinessRules rules, IStorageService storageService)
            {
                _readRepository = readRepository;
                _writeRepository = writeRepository;
                _rules = rules;
                _storageService = storageService;
            }

            public async Task<UploadDetailDocumentResponse> Handle(UploadDetailDocumentCommand request, CancellationToken cancellationToken)
            {

                FinanceExpenseDetail? file = await _readRepository.GetSingleAsync(u => u.Gid == request.Gid);

                _storageService.FileCopy(request.FileName, "Files/0temp", "Files/finance-expense-detail-documents");

                file.Document = "\\Files\\finance-expense-detail-documents\\" + request.FileName;

                _writeRepository.Update(file);
                await _writeRepository.SaveAsync();

                return new()
                {
                    FullPath = file.Document,
                    Title = UsersBusinessMessages.ProcessCompleted,
                    Message = AnnouncementsBusinessMessages.SuccessUploadImage,
                    IsValid = true
                };
            }


        }
    }
}