using Azure.Identity;
using EnterpriseAi.Api.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Load Azure App Configuration before services are registered
var appConfigEndpoint = builder.Configuration["AppConfig:Endpoint"];

if(!string.IsNullOrEmpty(appConfigEndpoint))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(new Uri(appConfigEndpoint), new DefaultAzureCredential())
               .ConfigureKeyVault(kv =>
               {
                   kv.SetCredential(new DefaultAzureCredential());
               })
               .ConfigureRefresh(refresh =>
               {
                   refresh.RegisterAll();
               });
                
        //Optional : if you later add lables, selection, refresh, feature flags, etc.
        //options.Select(keyFilter.Any, labelFilter.Null);
    });
}

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    //Azure App service / reverse proxy scenarios often need these cleared
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("angular", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",
            "https://victorious-pond-0475f930f.2.azurestaticapps.net")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseForwardedHeaders();

// Optional later if you enable dynamic refresh middleware
// app.UseAzureAppConfiguration();

app.UseApplicationPipeline();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
