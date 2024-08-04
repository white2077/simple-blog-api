using AspNetCoreRestfulApi.Core.RestExceptionAdvice;
using AspNetCoreRestfulApi.DBContext;
using AspNetCoreRestfulApi.Services;
using AspNetCoreRestfulApi.Services.Ipml;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDBContext>(option => option.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IBlogService,BlogService>();
builder.Services.AddScoped<IPostService,PostService>();
builder.Services.AddControllers(options =>
{
   options.Filters.Add<HttpResponseExceptionFilter>();
});

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
