using Corporate.Application.Services.Infrastructure;
using Corporate.Application.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Added for httpClientFactory
builder.Services.AddHttpClient("BooksServiceClient", config =>
{
    config.BaseAddress = new Uri("https://openlibrary.org/api/books/");
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});

builder.Services.AddHttpClient("CountryServiceClient", config =>
{
    config.BaseAddress = new Uri("https://restcountries.com/v3.1/");
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});

builder.Services.AddScoped<IServiceFactory, ServiceFactory<BooksService>>();
builder.Services.AddScoped<IServiceFactory, ServiceFactory<CountryService>>();

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
