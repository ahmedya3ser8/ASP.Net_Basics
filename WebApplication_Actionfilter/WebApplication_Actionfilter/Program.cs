using Microsoft.EntityFrameworkCore;
using WebApplication_Actionfilter.Data;
using WebApplication_Actionfilter.Filters;
using WebApplication_Actionfilter.Middelware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActivityFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(builder => builder.UseSqlServer("Server = .; Database = Products; Integrated Security = true; TrustServerCertificate = true;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RateLimitingMiddleware>();
app.UseMiddleware<ProfilingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
