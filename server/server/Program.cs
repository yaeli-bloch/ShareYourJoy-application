using Microsoft.EntityFrameworkCore;
using Server.Core.Repositories;
using Server.Core.Services;
using Server.Data;
using Server.Data.Repositories;
using Server.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// הגדרת ה-DbContext על בסיס ה-Connection String מקובץ ה-appsettings.json
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// הוספת ה-Repositories והשירותים שלך
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddDbContext<DataContext>();

// הוספת תמיכה ב-Controllers
builder.Services.AddControllers();

// הוספת Swagger/OpenAPI
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

app.Run();
