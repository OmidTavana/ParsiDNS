using Microsoft.EntityFrameworkCore;
using ParsiDNS.Core.Repository;
using ParsiDNS.Core.Repository.Services;
using ParsiDNS.Core.Timer;
using ParsiDNS.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContext
builder.Services.AddDbContext<ParsiDnsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ParsiDNS_ConnectionString"));
});
#endregion

#region IOC
builder.Services.AddScoped<IDnsRepository, DnsRepository>();


builder.Services.AddSingleton<IHostedService, TimedHostedService>(serviceProvider =>
{
    return new TimedHostedService(serviceProvider);
});

#endregion

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
