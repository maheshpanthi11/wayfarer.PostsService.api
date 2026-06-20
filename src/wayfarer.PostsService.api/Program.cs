
using Microsoft.EntityFrameworkCore;
using wayfarer.PostsService.api.Service;
using wayfarer.PostsService.DataAccess;
using wayfarer.PostsService.DataAccess.UnitOfWork;

namespace wayfarer.PostsService.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // UseNpgsql with environment variable expansion
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.ExpandEnvironmentVariables(connectionString);
            }
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Registering Unit of Work maps everything correctly
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IPostRepository, PostsRepository>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPostService, PostService>();


            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapOpenApi();

            // Wire up Swagger UI to read the native OpenAPI document
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "Wayfarer.PostsService.api v1");
            });

            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/swagger");
                await Task.CompletedTask;
            });

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Checks existence and creates database + schema instantly
                dbContext.Database.EnsureCreated();
                dbContext.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
