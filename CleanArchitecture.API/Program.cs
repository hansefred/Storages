using CleanArchitecture.Presentation;
using CleanArchitecture.Infastructure;
using CleanArchitecture.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPresentation()
                .AddInfastructure()
                .AddApplication();

builder.Host.UseSerilog((context,configure) =>
{
    configure.WriteTo.File("log.txt",rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.Run();
