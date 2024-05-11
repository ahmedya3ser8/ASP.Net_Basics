using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WebApplication_Authentication.Authentication;
using WebApplication_Authentication.Authorization;
using WebApplication_Authentication.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<PermissionBasedAuthorizationFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
// builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("SuperUsersOnly", builder =>
    //{
    //    builder.RequireRole("Admin", "SuperUser");
    //});

    //options.AddPolicy("EmployeeOnly", builder =>
    //{
    //    builder.RequireClaim("UserType", "Employee");
    //});

    //options.AddPolicy("AgeGreaterThan25", builder =>
    //{
    //    builder.RequireAssertion(context =>
    //    {
    //        var dob = DateTime.Parse(context.User.FindFirstValue("DateofBirth"));
    //        return DateTime.Today.Year - dob.Year > 25;
    //    });
    //});

    options.AddPolicy("AgeGreaterThan25", builder =>
    {
        builder.AddRequirements(new AgeGreaterThan25Requirement());
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorizationHandler>();

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
