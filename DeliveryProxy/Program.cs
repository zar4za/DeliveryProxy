using DeliveryProxy;
using DeliveryProxy.Auth;
using DeliveryProxy.Calculator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.Configure<CdekAuthOptions>(builder.Configuration.GetSection(nameof(CdekAuthOptions)));
builder.Services.AddSingleton<IHttpClientFactory, AuthorizedHttpClientFactory>();
builder.Services.AddScoped<CalculatorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

MappingProfile.CreateMappers();

app.Run();
