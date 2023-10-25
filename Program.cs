using Corporate.Application.Services.Config;
using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Services;
using Corporate.Application.Services.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Added for named httpClientFactory for books service
builder.Services.AddHttpClient("UserServiceClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://random-data-api.com/api/v2/users");
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
});

// Added for named httpClientFactory for country service
builder.Services.AddHttpClient("CountryServiceClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://restcountries.com/v3.1");
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
});

// Added for named httpClientFactory for country service
builder.Services.AddHttpClient("CreditcardServiceClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://random-data-api.com/api/v2");
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICreditcardService, CreditcardService>();

builder.Services.AddScoped<IServiceFactory<UserService>, ServiceFactory<UserService, UserServiceConfig>>();
builder.Services.AddScoped<IServiceFactory<CountryService>, ServiceFactory<CountryService, CountryServiceConfig>>();
builder.Services.AddScoped<IServiceFactory<CreditcardService>, ServiceFactory<CreditcardService, CreditcardServiceConfig>>();

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
