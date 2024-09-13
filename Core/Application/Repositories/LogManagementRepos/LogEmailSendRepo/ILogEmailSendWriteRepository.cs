using Core.Repositories.Abstracts;
using Domain.Entities.LogManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.LogManagementRepos.LogEmailSendRepo
{
    public interface ILogEmailSendWriteRepository : IWriteRepository<LogEmailSend>
    {

    }
}
