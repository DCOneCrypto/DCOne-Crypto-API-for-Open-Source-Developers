using Microsoft.EntityFrameworkCore;
using DCOneCrypto.Api.Services;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<NetworkService>();
builder.Services.AddScoped<EpochService>();
builder.Services.AddScoped<BlockService>();
builder.Services.AddScoped<TransactionsService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<PoolService>();
builder.Services.AddScoped<ScriptService>();
builder.Services.AddScoped<StakeAccountService>();
builder.Services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true).AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "DCOne Crypto Api Documentation",
            Version = "v1",
            Description = "This is a DCOne Crypto Api - Endpoint: https://gateway.dconecrypto.finance",
            TermsOfService = new Uri("https://dconecrypto.finance"),
            Contact = new OpenApiContact
            {
                Name = "Contact",
                Email = "admin@dconecrypto.finance",
                Url = new Uri("https://dconecrypto.finance/contact.html")
            },
            Extensions = new Dictionary<string, IOpenApiExtension>
            {
              {"x-logo", new OpenApiObject
                {
                   {"url", new OpenApiString("https://dconecrypto.finance/Common/Images/app-logo-on-dark.svg")},
                   { "altText", new OpenApiString("DCOne Crypto Api")}
                }
              }
            },
            License = new Microsoft.OpenApi.Models.OpenApiLicense
            {
                Name = "License",
                Url = new Uri("https://dconecrypto.finance")
            }
        });
}); ;

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(
    options =>
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "DConecrypto Demo Documentation v1"));

app.UseReDoc(options =>
{
    options.DocumentTitle = "DConecrypto Demo Documentation";
    options.SpecUrl = "/swagger/v1/swagger.json";
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
