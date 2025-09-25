using Microsoft.EntityFrameworkCore;
using Data;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
