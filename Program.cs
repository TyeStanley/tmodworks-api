using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using tmodworks_api.Core.Interfaces;
using tmodworks_api.Core.Models;
using tmodworks_api.Core.Services;
using tmodworks_api.Data;
using tmodworks_api.Data.Repositories;

namespace tmodworks_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            // Add Entity Framework Core with PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();

            // Register services
            builder.Services.AddScoped<ITodoService, TodoService>();

            // Add controllers
            builder.Services.AddControllers();

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });

            var app = builder.Build();

            // Ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();

                // Seed some initial data if the database is empty
                if (!context.Todos.Any())
                {
                    context.Todos.AddRange(
                        new Todo { Title = "Walk the dog", DueBy = DateOnly.FromDateTime(DateTime.Now) },
                        new Todo { Title = "Do the dishes", DueBy = DateOnly.FromDateTime(DateTime.Now) },
                        new Todo { Title = "Do the laundry", DueBy = DateOnly.FromDateTime(DateTime.Now.AddDays(1)) },
                        new Todo { Title = "Clean the bathroom" },
                        new Todo { Title = "Clean the car", DueBy = DateOnly.FromDateTime(DateTime.Now.AddDays(2)) }
                    );
                    context.SaveChanges();
                }
            }

            // Map controllers
            app.MapControllers();

            app.Run();
        }
    }

    [JsonSerializable(typeof(Todo[]))]
    [JsonSerializable(typeof(Todo))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
