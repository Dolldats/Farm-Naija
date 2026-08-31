using Application.Interface.Repository;
using Application.Interface.Services;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

using Application.Interface.Repository;
using Application.Interface.Services;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register FarmNaija DbContext
            builder.Services.AddDbContext<FarmNaijaDbcontext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(
                        builder.Configuration.GetConnectionString("DefaultConnection")
                    )
                ));

            // Register Order Services and Repositories
            builder.Services.AddScoped<IOrderServices, OrderService>();
            builder.Services.AddScoped<IOrderRepositories, OrderRepositories>();

            // OpenAPI
            builder.Services.AddOpenApi();

            // Register DbContext
            builder.Services.AddDbContext<FarmNaijaDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repositories
            builder.Services.AddScoped<IUserRepositories, UserRepositories>();

            // Register Services
            builder.Services.AddScoped<IUserServices, UserServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        "/openapi/v1.json",
                        "v1"
                    );
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}