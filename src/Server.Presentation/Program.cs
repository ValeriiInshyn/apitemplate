#region

using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
services.AddJwtAuthentication(configuration);
services.AddSwaggerConfiguration();
builder.Host.UseSerilog();


var app = builder.Build();


app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
        options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
});

app.UseRouting();

app.UseAuthorization();

app.AddMiddlewares();

app.MapControllers();

app.Run();