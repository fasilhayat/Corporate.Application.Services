using Corporate.Application.Services.Config;
using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Services;
using Corporate.Application.Services.Services.Interfaces;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load appsettings
var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false)
    .Build();

// Added for named httpClientFactory for books service
builder.Services.AddHttpClient($"{nameof(UserService)}Client", httpClient =>
{
    httpClient.BaseAddress = new Uri(config.GetSection(nameof(UserServiceConfig)).Get<UserServiceConfig>().BaseAddress!);
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseDefaultCredentials = true,
    Credentials = new NetworkCredential("", ""),
});

// Added for named httpClientFactory for country service
builder.Services.AddHttpClient($"{nameof(CountryService)}Client", httpClient =>
{
    httpClient.BaseAddress = new Uri(config.GetSection(nameof(CountryServiceConfig)).Get<CountryServiceConfig>().BaseAddress!);
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseDefaultCredentials = true,
    Credentials = new NetworkCredential("", ""),
});

// Added for named httpClientFactory for country service
builder.Services.AddHttpClient($"{nameof(CreditcardService)}Client", httpClient =>
{
    httpClient.BaseAddress = new Uri(config.GetSection(nameof(CreditcardServiceConfig)).Get<CreditcardServiceConfig>().BaseAddress!);
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseDefaultCredentials = true,
});

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<ICreditcardService, CreditcardService>();

builder.Services.AddTransient<IServiceFactory<UserService>, ServiceFactory<UserService>>();
builder.Services.AddTransient<IServiceFactory<CountryService>, ServiceFactory<CountryService>>();
builder.Services.AddTransient<IServiceFactory<CreditcardService>, ServiceFactory<CreditcardService>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
