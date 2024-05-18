using System.Reflection;
using CafeRevenueCalculatorApi.Endpoints;
using CafeRevenueCalculatorApi.Models;
using CafeRevenueCalculatorApi.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CafeRevenueCalculatorAPI",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddScoped<IValidator<RevenueCalculatorRequest>, RevenueCalculatorRequestValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.DocumentTitle = "CafeRevenueCalculatorAPI";
        config.RoutePrefix = "api/swagger";
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "CafeRevenueCalculatorAPI v1");
    });
}

app.MapRevenueCalculatorEndpoints();

app.UseSerilogRequestLogging();

app.Run();
