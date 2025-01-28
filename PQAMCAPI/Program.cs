using Helper;
using Microsoft.EntityFrameworkCore;
using PQAMCAPI.Interfaces.Services;
using PQAMCAPI.Services;
using PQAMCAPI.Providers.AuthHandlers.Scheme;
using PQAMCAPI.Providers.AuthHandlers;
using PQAMCAPI.Services.DB;
using ErrorHandling;
using PQAMCAPI.Services.Domain;
using API.Classes;
using Classes;
using Services;
using Hangfire;
using Hangfire.Oracle.Core;
using NLog;
using NLog.Web;
using PQAMCAPI.Services.TypedClients;
using Microsoft.AspNetCore.Mvc.Razor;
using PQAMCAPI.Interfaces;
using System.Security.Claims;
using PQAMCClasses;
using Newtonsoft.Json;
using PQAMCClasses.CloudDTOs;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// Add services to the container.
builder.Services.Configure<RazorViewEngineOptions>(o =>
{
    // {2} is area, {1} is controller,{0} is the action   
    o.ViewLocationFormats.Clear();
    o.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Views/Emails/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

String connStr = DBSettingsHelper.GetConnectionString(builder.Configuration);
builder.Services.AddDbContext<PQAMCAPIContext>(options => options.UseOracle(
    connStr,
    b => b.UseOracleSQLCompatibility("11")
));

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseFilter(new AutomaticRetryAttribute { Attempts = 1 })
        .UseStorage(new OracleStorage(
            DBSettingsHelper.GetConnectionString(builder.Configuration)
        //new OracleStorageOptions
        //{
        //    SchemaName = SchemaName.ToUpper()
        //}
        )));

builder.Services.AddHangfireServer(ops =>
{
    ops.WorkerCount = 1;
});

//builder.Services.AddDataProtection().PersistKeysToDbContext<PQAMCAPIContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(
    options => options.DefaultScheme = "PQAMCAuthScheme")
    .AddScheme<PQAMCAuthSchemeOptions, PQAMCAuthHandler>(
        "PQAMCAuthScheme", options => { });

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<SMSSettings>(builder.Configuration.GetSection("SMSSettings"));
builder.Services.Configure<OTPSettings>(builder.Configuration.GetSection("OTPSettings"));
builder.Services.Configure<ITMindsSettings>(builder.Configuration.GetSection("ITMindsSettings"));

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IStoreProcedureService, StoreProcedureService>();
builder.Services.AddTransient<ITokenDBService, TokenDBService>();
builder.Services.AddTransient<IUserDBService, UserDBService>();
builder.Services.AddTransient<IUserApplicationDBService, UserApplicationDBService>();
builder.Services.AddTransient<IUserBankDBService, UserBankDBService>();
builder.Services.AddTransient<IUserApplicationDocumentDBService, UserApplicationDocumentDBService>();
builder.Services.AddTransient<IUserApplicationDiscrepancyDBService, UserApplicationDiscrepancyDBService>();
builder.Services.AddTransient<IITMindsRequestDBService, ITMindsRequestDBService>();
builder.Services.AddTransient<IITMindsTokenDBService, ITMindsTokenDBService>();
builder.Services.AddTransient<IInvestmentRequestDBService, InvestmentRequestDBService>();
builder.Services.AddTransient<IRedemptionRequestDBService, RedemptionRequestDBService>();
builder.Services.AddTransient<IFundTransferRequestDBService, FundTransferRequestDBService>();
builder.Services.AddTransient<IAdminLoginDBService, AdminLoginDBService>();
builder.Services.AddTransient<IUserApplicationNomineeDBService, UserApplicationNomineeDBService>();
builder.Services.AddTransient<ISMSLogDBService, SMSLogDBService>();
builder.Services.AddTransient<IFundDBService, FundDBService>();
builder.Services.AddTransient<IInvestorTransactionDBService, InvestorTransactionDBService>();
builder.Services.AddTransient<IAMCFolioNumberDBService, AMCFolioNumberDBService>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IAreaService, AreaService>();
builder.Services.AddTransient<IBankService, BankService>();
builder.Services.AddTransient<IIncomeSourceService, IncomeSourceService>();
builder.Services.AddTransient<IEducationService, EducationService>();
builder.Services.AddTransient<IGenderService, GenderService>();
builder.Services.AddTransient<IProfessionService, ProfessionService>();
builder.Services.AddTransient<IOccupationService, OccupationService>();
builder.Services.AddTransient<IAccountCategoryService, AccountCategoryService>();
builder.Services.AddTransient<IContactOwnerShipService, ContactOwnerShipService>();
builder.Services.AddTransient<IAnnualIncomeService, AnnualIncomeService>();
builder.Services.AddTransient<IResidentialStatusService, ResidentialStatusService>();
builder.Services.AddTransient<ITINReasonService, TINReasonService>();
builder.Services.AddTransient<IUserApplicationService, UserApplicationService>();
builder.Services.AddTransient<IUserApplicationDocumentService, UserApplicationDocumentService>();
builder.Services.AddTransient<IUserApplicationDiscrepancyService, UserApplicationDiscrepancyService>();
builder.Services.AddTransient<IIBFTService, IBFTService>();
builder.Services.AddTransient<ICloudService, CloudService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IFundService, FundService>();
builder.Services.AddTransient<IInvestmentRequestService, InvestmentRequestService>();
builder.Services.AddTransient<IInvestmentService, InvestmentService>();
builder.Services.AddTransient<IRedemptionRequestService, RedemptionRequestService>();
builder.Services.AddTransient<IFundTransferRequestService, FundTransferRequestService>();
builder.Services.AddTransient<IITMindsClient, ITMindsClient>();
//builder.Services.AddTransient<IITMindsClient, ITMindsClientDummy>();
builder.Services.AddTransient<IUserBankService, UserBankService>();
builder.Services.AddTransient<IAdminLoginService, AdminLoginService>();
builder.Services.AddTransient<IVPSPlanService, VPSPlanService>();
builder.Services.AddTransient<IUserApplicationNomineeService, UserApplicationNomineeService>();
builder.Services.AddTransient<IITMindsService, ITMindsService>();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<ISMSService, PQSMSGateway>();
builder.Services.AddSingleton<IOTPService, OTPService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUserContextService, UserContextService>();

builder.Services.AddScoped<IEmailSender, EmailSender>(); // Scoped for template service
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<ISystemSettingsService, SystemSettingsService>();
builder.Services.AddScoped<ISystemSettingsDBService, SystemSettingsDBService>();

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseResponseCompression();

app.ConfigureExceptionHandler(app.Environment.IsDevelopment());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:3000", "http://localhost:3001", 
                        "http://localhost:*", "https://*.compass-dx.com",
                        "http://*.pakqatar.com.pk", "https://*.pakqatar.com.pk")
           .SetIsOriginAllowedToAllowWildcardSubdomains()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseRouting();

app.UseAuthorization();


app.Use(async (context, next) =>
{
    if (!(await SecurityHelper.VerifyContext(context)))
        throw new MyAPIException("Not Found");

    await next(context);
});


app.MapControllers();

app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello from 2nd delegate.");
//});

app.Run();
