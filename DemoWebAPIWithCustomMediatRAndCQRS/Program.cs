using CustomMediatR.Common.Handlers;
using CustomMediatR.Common.Interfaces;
using DemoWebAPIWithCustomMediatRAndCQRS;
using System.Reflection;
using FluentValidation;
using static FluentValidation.DependencyInjectionExtensions;
using Microsoft.AspNetCore.ResponseCompression; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IMediator, MediatR>();

// Register using reflection
builder.Services.AddCustomMediatRHandlers(Assembly.GetExecutingAssembly());
builder.Services.AddPipelineBehaviors(Assembly.GetExecutingAssembly());

// Register FluentValidation validators
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Add response compression services
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
});

// Configure compression options
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
// Use response compression middleware
app.UseResponseCompression();

app.Run();
