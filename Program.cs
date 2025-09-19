using Microsoft.EntityFrameworkCore;
using Data;

var builder = WebApplication.CreateSlimBuilder(args);

// Get the environment name
var env = builder.Environment.EnvironmentName;

// Add Entity Framework Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Map controllers
app.MapControllers();

app.Run();
