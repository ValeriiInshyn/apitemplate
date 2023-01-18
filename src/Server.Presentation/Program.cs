#region

using Serilog;
using Server.Presentation;

#endregion

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json").Build();

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSerilog(configuration);
services.AddRepositories();
services.AddServices();
services.AddApiVersioningSupport();

builder.Host.UseSerilog();


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


app.AddMiddlewares();

app.Run();