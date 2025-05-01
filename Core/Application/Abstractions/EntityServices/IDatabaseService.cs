using Application.Features.Base;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.EntityServices
{
    public interface IDatabaseService
    {
        public List<string> GetTableNames();
        public BaseResponse UpdateData(string tableName, Guid userGid, Guid gid, Dictionary<string, object> data);
        public List<ColumnData> GetTableData(string tableName, Guid id);
    }
    public class ColumnData
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}
