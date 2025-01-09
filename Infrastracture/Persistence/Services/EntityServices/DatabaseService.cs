using Application.Abstractions.EntityServices;
using Application.Features.Base;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Core.CrossCuttingConcern.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Persistence.Services.EntityServices
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        private readonly IUserReadRepository _userReadRepository;

        public DatabaseService(IConfiguration configuration, IUserReadRepository userReadRepository)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _userReadRepository = userReadRepository;
        }

        public List<string> GetTableNames()
        {
            var tableNames = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
            SELECT TABLE_NAME 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_TYPE = 'BASE TABLE' 
                AND TABLE_CATALOG = 'u1632502_emstv4' 
                AND TABLE_NAME NOT LIKE 'log%'
                AND TABLE_NAME != 'sysdiagrams'
                AND TABLE_NAME != '__EFMigrationsHistory';
        ";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableNames.Add(reader["TABLE_NAME"].ToString());
                        }
                    }
                }
            }

            return tableNames;
        }
        public BaseResponse UpdateData(string tableName, Guid userGid, Guid gid, Dictionary<string, object> data)
        {
            var user = _userReadRepository.GetByGid(userGid);

            if (user == null)
            {
                return new()
                {
                    IsValid = false,
                    Message = "User not found",
                    ActionType = "Update",
                    Title = "Update Data"
                };
            }
           

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string setClauses = string.Join(", ", data.Select(kvp => $"{kvp.Key} = '{kvp.Value}'"));
                var query = $"UPDATE {tableName} SET {setClauses} WHERE Gid = @Gid";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Gid", gid);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return new()
            {
                IsValid = true,
                Message = "Data updated successfully",
                ActionType = "Update",
                Title = "Update Data"
            };
        }
        public List<ColumnData> GetTableData(string tableName, Guid gid)
        {
            var columnDataList = new List<ColumnData>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM {tableName} WHERE Gid = @Gid;";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Gid", gid);
                    using (var reader = command.ExecuteReader())
                    {
                        var schemaTable = reader.GetSchemaTable();
                        while (reader.Read())
                        {
                            foreach (DataRow row in schemaTable.Rows)
                            {
                                columnDataList.Add(new ColumnData
                                {
                                    ColumnName = row["ColumnName"].ToString(),
                                    ColumnValue = reader[row["ColumnName"].ToString()].ToString()
                                });
                            }
                        }
                    }
                }
                connection.Close();
            }
            if (columnDataList.Count == 0)
                throw new BusinessException("Data not found");

            return columnDataList;
        }
    }
}
