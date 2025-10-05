using ChessEngine.Core;
using ChessWebApi.Data;
using ChessWebApi.Services;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// ChessService'i dependency injection’a ekle
builder.Services.AddScoped<ChessContext>();
builder.Services.AddTransient<IChessService, ChessService>();


builder.Services.AddDbContext<ChessDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// CORS yapýlandýrmasý
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();
app.Run();
