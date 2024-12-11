using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

//builder.Services.AddAuthorization(options =>
//{
    // By default, all incoming requests will be authorized according to the default policy.
 //   options.FallbackPolicy = options.DefaultPolicy;
//});

// Настройка Kestrel для использования сертификата
var configuration = builder.Configuration;
var certificatePath = configuration["Kestrel:Endpoints:Https:Certificate:Path"];
var certificatePassword = configuration["Kestrel:Endpoints:Https:Certificate:Password"];

// Если сертификат уже установлен, можно использовать его напрямую
var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
store.Open(OpenFlags.ReadOnly);
var certificate = store.Certificates.Find(X509FindType.FindByThumbprint, "B8C79E2E1568D91907C975505185ED3E4EA07BCE", false)[0];
store.Close();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7183, listenOptions =>
    {
        listenOptions.UseHttps(certificate);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();