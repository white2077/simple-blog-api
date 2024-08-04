using System.Text;
using AspNetCoreRestfulApi.Core.RestExceptionAdvice;
using AspNetCoreRestfulApi.Data;
using AspNetCoreRestfulApi.Entities;
using AspNetCoreRestfulApi.Services;
using AspNetCoreRestfulApi.Services.Ipml;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add database
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");

// Add services
builder.Services.AddDbContext<AppDbContext>(option => option.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IBlogService,BlogService>();
builder.Services.AddScoped<IPostService,PostService>();

// Add Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(op =>
{
    op.Password.RequireDigit = false;
    op.Password.RequireLowercase = false;
    op.Password.RequireUppercase = false;
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequiredLength = 6;
    op.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();

//Config cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500","https://8327-1-53-185-173.ngrok-free.app")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Add Jwt
builder.Services.AddAuthentication(op =>
{
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
    };
});

//Rest Controller Advice
builder.Services.AddControllers(options =>
{
   options.Filters.Add<HttpResponseExceptionFilter>();
});

var app = builder.Build();

//WebSocket Server
app.MapHub<NotificationService>("/notification");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseAuthorization();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
