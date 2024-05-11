using Microsoft.EntityFrameworkCore;
using WebApplication_Configuration.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

// 1)  Not The Best Way
// var attachmentOptions = builder.Configuration.GetSection("Attachments").Get<AttachmentOptions>();
// builder.Services.AddSingleton(attachmentOptions);

// 2)  Not The Best Way
// var attachmentOptions = new AttachmentOptions(); 
// builder.Configuration.GetSection("Attachments").Bind(attachmentOptions);
// builder.Services.AddSingleton(attachmentOptions);

// 3) Options Pattern The Best Way
builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments"));

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
