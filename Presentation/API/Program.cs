using API.Filters;
using Application;
using Application.Features.AuthManagementFeatures.AuthPages.Profiles;
using Core;
using Core.Context;
using Core.CrossCuttingConcern.Exceptions.WepApi.Extensions;
using Core.Infrastracture;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.MemoryStorage;
using Infrastracture;
using Infrastracture.Services.Storage.Local;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Context;
using Persistence.HangfireJobs.DatabaseBackUp;
using Persistence.HangfireJobs.FileSystem;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

#region hangfire 
builder.Services.AddHangfire(x =>
{
    x.UseMemoryStorage();

    RecurringJob.AddOrUpdate<HangfireRemoveFile>(j => j.RemoveFile(), "0 2 * * 7", TimeZoneInfo.Local);
    RecurringJob.AddOrUpdate<DatabaseBackUp>(j => j.BackUpDB_LogTables(), "0 2 * * 7", TimeZoneInfo.Local);
    RecurringJob.AddOrUpdate<DatabaseBackUp>(j => j.BackUpDB_OtherAllTables(), "0 2 * * 7", TimeZoneInfo.Local);
    RecurringJob.AddOrUpdate<DatabaseBackUp>(j => j.BackUpDB_AuthTables(), "0 2 * * 7", TimeZoneInfo.Local);

    //her pazar saat gece iki de

});
builder.Services.AddHangfireServer();


#endregion

#region log

var columnOpt = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
                   {
                        new SqlColumn {ColumnName = "UserName",DataType = SqlDbType.NVarChar, DataLength = 250 },
                        new SqlColumn {ColumnName = "GidUserFK", DataType = SqlDbType.NVarChar,DataLength=50},
                        new SqlColumn {ColumnName = "Path", DataType = SqlDbType.NVarChar, DataLength = 250},
                        new SqlColumn {ColumnName = "Action", DataType = SqlDbType.NVarChar, DataLength = 250},
                        new SqlColumn {ColumnName = "MethodName", DataType = SqlDbType.NVarChar, DataLength = 250},
                        new SqlColumn {ColumnName = "QueryString", DataType=SqlDbType.NVarChar, DataLength= 200},
                        new SqlColumn {ColumnName = "StackTrace", DataType = SqlDbType.NVarChar, DataLength = -1},
                        new SqlColumn {ColumnName = "FileName", DataType = SqlDbType.NVarChar, DataLength = 200},
                        new SqlColumn {ColumnName = "LineNumber", DataType = SqlDbType.Int},
                   }
};
columnOpt.Store.Remove(StandardColumn.Exception);
columnOpt.Store.Remove(StandardColumn.MessageTemplate);




Logger log = new LoggerConfiguration()
.WriteTo.MSSqlServer(
               connectionString: connectionString, // MSSQL baðlantý dizesi
               sinkOptions: new MSSqlServerSinkOptions
               {
                   TableName = "LogErrors", // Tablo adý
                   AutoCreateSqlTable = true // Tabloyu otomatik oluþtur
               },
               columnOptions: columnOpt
               ).MinimumLevel.Error()
               .Enrich.
               FromLogContext().CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

// Minimum log seviyesini belirle
//.WriteTo.Seq("Url") // Seq'e log gönder
//.Enrich.FromLogContext() // Log baðlamýný zenginleþtir
//.MinimumLevel.Information() // Minimum log seviyesini belirle
//.CreateLogger();

#endregion

#region Controller - Fluent Validation - Json

builder.Services.AddControllers().AddFluentValidation(configuration =>
{
    configuration.DisableDataAnnotationsValidation = true;

    // YoneticiValidator sýnýfýnýn bulunduðu derlemedeki doðrulayýcýlarý FluentValidation yapýlandýrmasýna kaydeder. Bu þekilde, belirtilen derlemedeki tüm doðrulayýcýlar otomatik olarak kullanýlabilir hale gelir.
    //configuration.RegisterValidatorsFromAssemblyContaining<YoneticiValidator>(); -->validatorleri yazdýktan sonra eklicez

    //otomatik doðrulama özelliðini etkinleþtirir. Bu, bir HTTP isteði alýndýðýnda, doðrulama kurallarýnýn otomatik olarak uygulanmasýný saðlar.
    configuration.AutomaticValidationEnabled = true;
})
.AddJsonOptions(configurations =>
{
    // Serileþtirmesinde döngüsel referanslarý iþlemek için referans iþleyiciyi ayarlar. IgnoreCycles, döngüsel referanslarý görmezden gelerek olasý bir sonsuz döngüyü önler.
    configurations.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
#endregion

#region Api Versioning & Api Explorer
// IServiceCollection arabirimine ApiVersioning hizmetini ekler. 
builder.Services.AddApiVersioning(options =>
{
    // Varsayýlan API sürümünü ayarlar. Burada 1.0 sürümü, projenin varsayýlan API sürümü olarak belirlenir.
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Belirtilmeyen durumlarda varsayýlan API sürümünün kullanýlmasýný saðlar. Yani istemci bir API sürümü belirtmezse, options.DefaultApiVersion ile belirtilen varsayýlan sürüm kullanýlýr.
    options.AssumeDefaultVersionWhenUnspecified = true;

    // API sürümlerini yanýtta raporlama ayarýný etkinleþtirir. Bu, API yanýtlarýnda kullanýlan sürüm bilgisini gönderir.
    options.ReportApiVersions = true;
});

// IServiceCollection arabirimine VersionedApiExplorer hizmetini ekler. 
builder.Services.AddVersionedApiExplorer(options =>
{
    // API grup adý biçimini belirler. 'v'VVV formatý kullanýlarak grup adlarý oluþturulur. VVV, API sürümünü temsil eden yer tutucudur.
    options.GroupNameFormat = "'v'VVV";

    // API sürümünü URL içinde yerine koyma ayarýný etkinleþtirir. Böylece, API isteklerinde URL içindeki sürüm belirtilmiþ olur.
    options.SubstituteApiVersionInUrl = true;
});
#endregion

#region Swagger
// Swagger yapýlandýrma seçeneklerini SwaggerGenOptions için yapýlandýran ConfigureSwaggerOptions sýnýfýný IConfigureOptions arabirimine ekler. Bu, Swagger belgelendirmesinin yapýlandýrýlmasýný saðlar.
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// IServiceCollection arabirimine SwaggerGen hizmetini ekler.
builder.Services.AddSwaggerGen(options =>
{
    // Mevcut projenin XML belgelendirme dosyasýnýn adýný oluþturur. Bu dosya, projenin içinde API hakkýnda detaylý bilgileri içerir.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    // XML belgelendirme dosyasýnýn tam yolunu oluþturur. AppContext.BaseDirectory, uygulamanýn çalýþtýðý dizini temsil eder.
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Swagger belgelendirmesine XML belgelendirme dosyasýný dahil etme ayarýný yapar. Bu sayede, API Controller'larýndaki örnekler, parametreler ve dönüþ deðerleri gibi detaylý açýklamalarý Swagger belgelerine ekler.
});
#endregion

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidateAudience = true, //oluþturulacak tokenin kimlerin/ hangi sitelerin/ hangi originlerin kullanacaðýný belirlediðimiz deðerdir(www.lawyerclient.com)
            ValidateIssuer = true,  //oluþturulacak  tokenin kimin daðýtýðýmýný ifade eden alandýr(www.lawyerapi.com)
            ValidateLifetime = true, //oluþturulacak tokenin süresini kontrol edecek alandýr
            ValidateIssuerSigningKey = true, //üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr 

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });
#endregion

#region Cors
// IServiceCollection arabirimine CORS (Cross-Origin ResourceFK Sharing) hizmetini ekler. CORS, web uygulamalarýnýn farklý kaynaklardan gelen isteklere izin vermesini saðlayan bir mekanizmadýr.
// CORS hizmetini eklemek, Web API'nin farklý etki alanlarýndan gelen istekleri kabul etmesini ve gerekirse yanýtlara uygun CORS baþlýklarýný eklemesini saðlar. Bu þekilde, Web API'ye dýþ kaynaklardan eriþim saðlanabilir.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
policy.WithOrigins("http://localhost:5173", "https://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));
#endregion

#region DbContext
//IServiceCollection arabirimine XXXContext tipinde bir veritabaný baðlamýný (DbContext) ekler.
builder.Services.AddDbContext<Emasist2024Context>(options =>
{
    // SQL Server veritabaný saðlayýcýsýný kullanarak veritabaný baðlamýnýn yapýlandýrýlmasýný yapar. connectionString parametresi, SQL Server baðlantý dizesini temsil eder. 
    // b => b.MigrationsAssembly("SalihPoc.Api") ifadesi, veritabaný migrasyonlarýný uygulamak için kullanýlacak olan migrasyon derlemesinin adýný belirtir. Bu ad, "SalihPoc.Api" olarak belirtilmiþtir.

    options.UseSqlServer(connectionString);
});
builder.Services.AddDbContext<Context>(options =>
{
    // SQL Server veritabaný saðlayýcýsýný kullanarak veritabaný baðlamýnýn yapýlandýrýlmasýný yapar. connectionString parametresi, SQL Server baðlantý dizesini temsil eder. 
    // b => b.MigrationsAssembly("SalihPoc.Api") ifadesi, veritabaný migrasyonlarýný uygulamak için kullanýlacak olan migrasyon derlemesinin adýný belirtir. Bu ad, "SalihPoc.Api" olarak belirtilmiþtir.

    options.UseSqlServer(connectionString);
});
#endregion 
builder.Services.AddContainerWithDependenciesApplication();
builder.Services.AddContainerWithDependenciesPersistence();
builder.Services.AddContainerWithDependenciesCore();

builder.Services.AddContainerWithDependenciesInfrastucture();

builder.Services.AddStorage<LocalStorage>();
builder.Services.AddScoped<CustomAuthorizationFilter>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSerilogRequestLogging();
app.UseHttpLogging();
//if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.UseCors();
app.UseSession();
app.UseAuthentication();
app.UseStaticFiles();
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
//app.UseMiddleware<SessionMiddleware>();

app.MapControllers();
app.UseHangfireDashboard();
app.Run();
