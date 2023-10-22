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
builder.Services.AddHttpClient("BookServiceClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://openlibrary.org/api/books/");
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
});

// Added for named httpClientFactory for country service
builder.Services.AddHttpClient("CountryServiceClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://restcountries.com/v3.1/");
    httpClient.Timeout = new TimeSpan(0, 0, 30);
    httpClient.DefaultRequestHeaders.Clear();
});

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICountryService, CountryService>();

builder.Services.AddScoped<IServiceFactory<BookService>, ServiceFactory<BookService>>();
builder.Services.AddScoped<IServiceFactory<CountryService>, ServiceFactory<CountryService>>();

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
