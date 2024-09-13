using Core.CrossCuttingConcern.Exceptions.WepApi.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Drawing.Drawing2D;
using System.Security.Claims;

namespace Core.CrossCuttingConcern.Exceptions.WepApi.Middleware
{

    public class ExceptionMiddleware
    {
        private string JsonData { get; set; }
        private string Method { get; set; }

        private readonly IHttpContextAccessor _contextAccessor;


        private readonly HttpExceptionHandler _httpExceptionHandler;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IConfiguration _configuration;
        //private readonly a.ILogger _loggerService;

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, /*a.ILogger loggerService*/ ILogger<ExceptionMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _contextAccessor = contextAccessor;
            // _loggerService = loggerService;
            _httpExceptionHandler = new HttpExceptionHandler();
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                Method = context.Request.Method;

                //Eðer pageIndex ve size queryleri doluysa odeðerleri, dolu deðilse default 0 ve 10 deðerlerini atýyor
                string pageIndex = context.Request.Query["PageIndex"].Count == 0 ? "0" : context.Request.Query["PageIndex"][0];
                string pageSize = context.Request.Query["PageSize"].Count == 0 ? "10" : context.Request.Query["PageSize"][0];

                //Eðer metod GET ise querystringten JsonData alýyor. Eðer metod GET deðilse bodyden alýyor.
                if (Method == "GET")
                {
                    //LogUserPageVisit doldurulabilir

                    JsonData = $"{{\n \"PageIndex\": {pageIndex},\n \"PageSize\": {pageSize}\n}}";
                }
                else
                {
                    //EnableBuffering yaparak context.Request.Body.Position deðerini deðiþtirebiliyoruz.
                    //Position deðerini deðiþtiremezsek eðer body tekrar okunamýyor.
                    context.Request.EnableBuffering();
                    JsonData = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                await _next(context);

                await LogUserPageVisitAction(context);
            }
            catch (System.Exception exception)
            {
                await LogException(context, exception);
                await HandleExceptionAsync(context.Response, exception);
            }
        }

        private Task HandleExceptionAsync(HttpResponse response, System.Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleException(exception);
        }

        protected virtual async Task LogException(HttpContext context, System.Exception exception)
        {
            //var userName = context.User.Identity?.Name ?? null;
            var gid = context.User.Identity?.Name ?? null;
            string fullName = "anonymous";

            if (gid != null)
            {
                string _connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT Name, SurName FROM Users WHERE Gid = @Gid";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Gid", gid);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            string firstName = reader["Name"].ToString();
                            string lastName = reader["SurName"].ToString();
                            fullName = firstName + " " + lastName;

                        }
                        else
                        {
                            fullName = "anonymous";
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                
            }

            string stackTrace = exception.StackTrace;
            string[] lines = stackTrace.Split("\n");

            // Stack trace'deki ilk satýr hatanýn kaynaðýný gösterir
            string firstLine = lines[0].Trim();
            int lineNumber = -1;

            // Satýr numarasýný alma
            int lineIndex = firstLine.LastIndexOf(":line");
            if (lineIndex != -1)
            {
                int startIndex = lineIndex + 6; // ":line" kelimesinin sonraki indeksi
                string lineString = firstLine.Substring(startIndex).Trim();
                lineNumber = int.Parse(lineString);
            }

            // Dosya adýný alma
            int fileNameIndex = firstLine.LastIndexOf(" in ");
            string fileName = "";
            if (fileNameIndex != -1)
            {
                fileName = firstLine.Substring(fileNameIndex + 4).Trim();
            }



            var logDetail = new
            {
                MethodName = _next.Method.Name,
                Action = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                UserName = fullName,
                StackTrace = exception.StackTrace,
                Timestamp = DateTime.Now,
                FileName = fileName,
                LineNumber = lineNumber,
                ErrorCode = exception.HResult,

            };
            LogContext.PushProperty("MethodName", logDetail.MethodName);
            LogContext.PushProperty("UserName", logDetail.UserName);
            LogContext.PushProperty("StackTrace", logDetail.StackTrace);
            LogContext.PushProperty("QueryString", logDetail.QueryString);
            LogContext.PushProperty("Path", logDetail.Path);
            LogContext.PushProperty("GidUserFK", gid);
            LogContext.PushProperty("Action", logDetail.Action);
            LogContext.PushProperty("FileName", logDetail.FileName);
            LogContext.PushProperty("LineNumber", logDetail.LineNumber);

            _logger.LogError(exception.Message, logDetail);

        }

        private async Task LogUserPageVisitAction(HttpContext context)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                string userGid = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userGid == null)
                {
                    return;
                }

                string ipAddress = context.Connection.RemoteIpAddress?.ToString();
                string pageInfo = context.Request.Headers["Page"].ToString();
                string operation = context.Request.Path;
                Guid guid = Guid.NewGuid(); 

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"
                   INSERT INTO LogUserPageVisitActions (Gid,GidUserFK, IpAddress, PageInfo, Operation, JSonData, CreatedDate, DataState)
                   SELECT @Gid, @GidUserFK, @IpAddress, @PageInfo, @Operation, @JSonData, @CreatedDate, @DataState";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Gid", guid);
                    command.Parameters.AddWithValue("@GidUserFK", userGid);
                    command.Parameters.AddWithValue("@IpAddress", ipAddress);
                    command.Parameters.AddWithValue("@PageInfo", pageInfo);
                    command.Parameters.AddWithValue("@Operation", operation);
                    command.Parameters.AddWithValue("@JSonData", JsonData);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@DataState", 1);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                await LogException(context, ex);
            }
        }
    }
}
