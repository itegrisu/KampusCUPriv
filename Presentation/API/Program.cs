using API.Filters;
using Application;
using Application.Features.GeneralFeatures.Users.Profiles;
using Core;
using Core.Context;
using Core.CrossCuttingConcern.Exceptions.WepApi.Extensions;
using Core.Infrastracture;
using FirebaseAdmin;
using FluentValidation.AspNetCore;
using Google.Apis.Auth.OAuth2;
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
builder.Logging.AddConsole();

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
               connectionString: connectionString, // MSSQL ba�lant� dizesi
               sinkOptions: new MSSqlServerSinkOptions
               {
                   TableName = "LogErrors", // Tablo ad�
                   AutoCreateSqlTable = true // Tabloyu otomatik olu�tur
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
//.WriteTo.Seq("Url") // Seq'e log g�nder
//.Enrich.FromLogContext() // Log ba�lam�n� zenginle�tir
//.MinimumLevel.Information() // Minimum log seviyesini belirle
//.CreateLogger();

#endregion

#region Controller - Fluent Validation - Json

builder.Services.AddControllers().AddFluentValidation(configuration =>
{
    configuration.DisableDataAnnotationsValidation = true;

    // YoneticiValidator s�n�f�n�n bulundu�u derlemedeki do�rulay�c�lar� FluentValidation yap�land�rmas�na kaydeder. Bu �ekilde, belirtilen derlemedeki t�m do�rulay�c�lar otomatik olarak kullan�labilir hale gelir.
    //configuration.RegisterValidatorsFromAssemblyContaining<YoneticiValidator>(); -->validatorleri yazd�ktan sonra eklicez

    //otomatik do�rulama �zelli�ini etkinle�tirir. Bu, bir HTTP iste�i al�nd���nda, do�rulama kurallar�n�n otomatik olarak uygulanmas�n� sa�lar.
    configuration.AutomaticValidationEnabled = true;
})
.AddJsonOptions(configurations =>
{
    // Serile�tirmesinde d�ng�sel referanslar� i�lemek i�in referans i�leyiciyi ayarlar. IgnoreCycles, d�ng�sel referanslar� g�rmezden gelerek olas� bir sonsuz d�ng�y� �nler.
    configurations.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
#endregion

#region Api Versioning & Api Explorer
// IServiceCollection arabirimine ApiVersioning hizmetini ekler. 
builder.Services.AddApiVersioning(options =>
{
    // Varsay�lan API s�r�m�n� ayarlar. Burada 1.0 s�r�m�, projenin varsay�lan API s�r�m� olarak belirlenir.
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Belirtilmeyen durumlarda varsay�lan API s�r�m�n�n kullan�lmas�n� sa�lar. Yani istemci bir API s�r�m� belirtmezse, options.DefaultApiVersion ile belirtilen varsay�lan s�r�m kullan�l�r.
    options.AssumeDefaultVersionWhenUnspecified = true;

    // API s�r�mlerini yan�tta raporlama ayar�n� etkinle�tirir. Bu, API yan�tlar�nda kullan�lan s�r�m bilgisini g�nderir.
    options.ReportApiVersions = true;
});

// IServiceCollection arabirimine VersionedApiExplorer hizmetini ekler. 
builder.Services.AddVersionedApiExplorer(options =>
{
    // API grup ad� bi�imini belirler. 'v'VVV format� kullan�larak grup adlar� olu�turulur. VVV, API s�r�m�n� temsil eden yer tutucudur.
    options.GroupNameFormat = "'v'VVV";

    // API s�r�m�n� URL i�inde yerine koyma ayar�n� etkinle�tirir. B�ylece, API isteklerinde URL i�indeki s�r�m belirtilmi� olur.
    options.SubstituteApiVersionInUrl = true;
});
#endregion

#region Swagger
// Swagger yap�land�rma se�eneklerini SwaggerGenOptions i�in yap�land�ran ConfigureSwaggerOptions s�n�f�n� IConfigureOptions arabirimine ekler. Bu, Swagger belgelendirmesinin yap�land�r�lmas�n� sa�lar.
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// IServiceCollection arabirimine SwaggerGen hizmetini ekler.
builder.Services.AddSwaggerGen(options =>
{
    // Mevcut projenin XML belgelendirme dosyas�n�n ad�n� olu�turur. Bu dosya, projenin i�inde API hakk�nda detayl� bilgileri i�erir.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    // XML belgelendirme dosyas�n�n tam yolunu olu�turur. AppContext.BaseDirectory, uygulaman�n �al��t��� dizini temsil eder.
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Swagger belgelendirmesine XML belgelendirme dosyas�n� dahil etme ayar�n� yapar. Bu sayede, API Controller'lar�ndaki �rnekler, parametreler ve d�n�� de�erleri gibi detayl� a��klamalar� Swagger belgelerine ekler.
});
#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Admin"; // Varsayılan kimlik doğrulama şeması
    options.DefaultChallengeScheme = "Admin";    // Varsayılan zorlama şeması
    options.DefaultScheme = "Admin";             // Genel varsayılan şema
})
.AddJwtBearer("Admin", option =>
{
    option.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.NameIdentifier
    };
});
#endregion

#region Cors
// IServiceCollection arabirimine CORS (Cross-Origin ResourceFK Sharing) hizmetini ekler. CORS, web uygulamalar�n�n farkl� kaynaklardan gelen isteklere izin vermesini sa�layan bir mekanizmad�r.
// CORS hizmetini eklemek, Web API'nin farkl� etki alanlar�ndan gelen istekleri kabul etmesini ve gerekirse yan�tlara uygun CORS ba�l�klar�n� eklemesini sa�lar. Bu �ekilde, Web API'ye d�� kaynaklardan eri�im sa�lanabilir.
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin() // T�m kaynaklara izin verir
              .AllowAnyHeader()  // T�m ba�l�klara izin verir
              .AllowAnyMethod()  // T�m HTTP metodlar�na izin verir
    )
);
#endregion

#region DbContext
//IServiceCollection arabirimine XXXContext tipinde bir veritaban� ba�lam�n� (DbContext) ekler.
builder.Services.AddDbContext<KampusCUContext>(options =>
{
    // SQL Server veritaban� sa�lay�c�s�n� kullanarak veritaban� ba�lam�n�n yap�land�r�lmas�n� yapar. connectionString parametresi, SQL Server ba�lant� dizesini temsil eder. 
    // b => b.MigrationsAssembly("SalihPoc.Api") ifadesi, veritaban� migrasyonlar�n� uygulamak i�in kullan�lacak olan migrasyon derlemesinin ad�n� belirtir. Bu ad, "SalihPoc.Api" olarak belirtilmi�tir.

    options.UseSqlServer(connectionString);
});
builder.Services.AddDbContext<Context>(options =>
{
    // SQL Server veritaban� sa�lay�c�s�n� kullanarak veritaban� ba�lam�n�n yap�land�r�lmas�n� yapar. connectionString parametresi, SQL Server ba�lant� dizesini temsil eder. 
    // b => b.MigrationsAssembly("SalihPoc.Api") ifadesi, veritaban� migrasyonlar�n� uygulamak i�in kullan�lacak olan migrasyon derlemesinin ad�n� belirtir. Bu ad, "SalihPoc.Api" olarak belirtilmi�tir.

    options.UseSqlServer(connectionString);
});
#endregion 

void InitializeFirebase()
{
    string basePath = AppContext.BaseDirectory;
    string credentialPath = Path.Combine(basePath, "kampuscu-6c1ae-firebase-adminsdk-fbsvc-56d05a892b.json");

    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile(credentialPath)
    });
}

InitializeFirebase();

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
app.Logger.LogInformation("Uygulama başlatıldı!");

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
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
