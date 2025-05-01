using Application.Abstractions.EntityServices;

namespace Persistence.Services.EntityServices
{
    public class LogService : ILogService
    {
        //    private readonly string _connectionString;
        //    private readonly ILogSuccessedLoginReadRepository _logSuccessedLoginReadRepository;
        //    private readonly ILogSuccessedLoginWriteRepository _logSuccessedLoginWriteRepository;

        //    public LogService(IConfiguration configuration, ILogSuccessedLoginReadRepository logSuccessedLoginReadRepository, ILogSuccessedLoginWriteRepository logSuccessedLoginWriteRepository)
        //    {
        //        _connectionString = configuration.GetConnectionString("DefaultConnection"); ;
        //        _logSuccessedLoginReadRepository = logSuccessedLoginReadRepository;
        //        _logSuccessedLoginWriteRepository = logSuccessedLoginWriteRepository;
        //    }

        //    public List<LogErrorDto> GetLogErrors(Guid? userGid, DateTime startTime, DateTime endTime)
        //    {
        //        List<LogErrorDto> logErrors = new List<LogErrorDto>();

        //        if(userGid != null)
        //        {
        //            using (SqlConnection connection = new SqlConnection(_connectionString))
        //            {
        //                string query = @"SELECT Id, UserName, GidUserFK, TimeStamp, LineNumber, Action, FileName, Message, StackTrace 
        //                         FROM LogErrors 
        //                         WHERE GidUserFK = @UserGid AND TimeStamp BETWEEN @StartTime AND @EndTime";
        //                SqlCommand command = new SqlCommand(query, connection);
        //                command.Parameters.AddWithValue("@UserGid", userGid);
        //                command.Parameters.AddWithValue("@StartTime", startTime);
        //                command.Parameters.AddWithValue("@EndTime", endTime);

        //                try
        //                {
        //                    connection.Open();
        //                    SqlDataReader reader = command.ExecuteReader();
        //                    while (reader.Read())
        //                    {
        //                        LogErrorDto logError = new LogErrorDto
        //                        {
        //                            Id = Convert.ToInt32(reader["Id"]),
        //                            UserName = reader["UserName"].ToString(),
        //                            GidUserFK = reader["GidUserFK"].ToString(),
        //                            TimeStamp = reader["TimeStamp"].ToString(),
        //                            LineNumber = Convert.ToInt32(reader["LineNumber"]),
        //                            Action = reader["Action"].ToString(),
        //                            FileName = reader["FileName"].ToString(),
        //                            ErrorMessage = reader["Message"].ToString(),
        //                            StackTrace = reader["StackTrace"].ToString()
        //                        };
        //                        logErrors.Add(logError);
        //                    }
        //                    reader.Close();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine("An error occurred: " + ex.Message);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            using (SqlConnection connection = new SqlConnection(_connectionString))
        //            {
        //                string query = @"SELECT Id, UserName, GidUserFK, TimeStamp, LineNumber, Action, FileName, Message, StackTrace 
        //                         FROM LogErrors 
        //                         WHERE TimeStamp BETWEEN @StartTime AND @EndTime";
        //                SqlCommand command = new SqlCommand(query, connection);
        //                command.Parameters.AddWithValue("@StartTime", startTime);
        //                command.Parameters.AddWithValue("@EndTime", endTime);

        //                try
        //                {
        //                    connection.Open();
        //                    SqlDataReader reader = command.ExecuteReader();
        //                    while (reader.Read())
        //                    {
        //                        LogErrorDto logError = new LogErrorDto
        //                        {
        //                            Id = Convert.ToInt32(reader["Id"]),
        //                            UserName = reader["UserName"].ToString(),
        //                            GidUserFK = reader["GidUserFK"].ToString(),
        //                            TimeStamp = reader["TimeStamp"].ToString(),
        //                            LineNumber = Convert.ToInt32(reader["LineNumber"]),
        //                            Action = reader["Action"].ToString(),
        //                            FileName = reader["FileName"].ToString(),
        //                            ErrorMessage = reader["Message"].ToString(),
        //                            StackTrace = reader["StackTrace"].ToString()
        //                        };
        //                        logErrors.Add(logError);
        //                    }
        //                    reader.Close();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine("An error occurred: " + ex.Message);
        //                }
        //            }
        //        }



        //        return logErrors;
        //    }

        //    public async Task LogOutLog(Guid gid)
        //    {
        //        LogSuccessedLogin logSuccessedLogin = _logSuccessedLoginReadRepository.GetByGid(gid);
        //        if (logSuccessedLogin == null)
        //        {
        //            throw new Exception("Log not found");
        //        }
        //        logSuccessedLogin.LogOutDate = DateTime.Now;
        //        _logSuccessedLoginWriteRepository.Update(logSuccessedLogin);
        //       await _logSuccessedLoginWriteRepository.SaveAsync();


        //    }
        //}
    }
}