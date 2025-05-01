using Core.CrossCuttingConcern.Logging.Configurations;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcern.Logging.Serilog.File
{
    public class SerilogFileLogger : SerilogLoggerServiceBase
    {



        public SerilogFileLogger()
            : base(logger: null!)
        {









            var columnOpt = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                   {
                        new SqlColumn {ColumnName = "message", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "message_template", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "level", DataType = SqlDbType.NVarChar, DataLength = 128},
                        new SqlColumn {ColumnName = "time_stamp", DataType = SqlDbType.DateTimeOffset},
                        new SqlColumn {ColumnName = "exception", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "log_event", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "user_name", DataType = SqlDbType.NVarChar, DataLength = 256}
                   }
            };
            columnOpt.Store.Remove(StandardColumn.Exception);
            columnOpt.Store.Remove(StandardColumn.MessageTemplate);



            Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
               connectionString: "Server=94.73.151.19;Database=u1632502_nam2;User Id=u1632502_nam2;Password=4dw5DgaJY4A_8-.5@;TrustServerCertificate=True;Trusted_Connection=False;", // MSSQL baðlantý dizesi
               sinkOptions: new MSSqlServerSinkOptions
               {
                   TableName = "logs", // Tablo adý
                   AutoCreateSqlTable = true // Tabloyu otomatik oluþtur
               },
               columnOptions: new ColumnOptions
               {
                   AdditionalColumns = new Collection<SqlColumn>
                   {
                        new SqlColumn {ColumnName = "message", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "message_template", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "level", DataType = SqlDbType.NVarChar, DataLength = 128},
                        new SqlColumn {ColumnName = "time_stamp", DataType = SqlDbType.DateTimeOffset},
                        new SqlColumn {ColumnName = "exception", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "log_event", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "user_name", DataType = SqlDbType.NVarChar, DataLength = 256}
                   }
               }
              
               ,
               restrictedToMinimumLevel: LogEventLevel.Information).CreateLogger();// Minimum log seviyesini belirle
                                                                                   //.WriteTo.Seq("Url") // Seq'e log gönder
                                                                                   //.Enrich.FromLogContext() // Log baðlamýný zenginleþtir
                                                                                   //.MinimumLevel.Information() // Minimum log seviyesini belirle
                                                                                   //.CreateLogger();


        }
    }
}
