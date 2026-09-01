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

            // Add services to the container
            builder.Services.AddControllers();

            // Register FarmNaija DbContext
            builder.Services.AddDbContext<FarmNaijaDbcontext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(
                        builder.Configuration.GetConnectionString("DefaultConnection")
                    )
                )
            );

            // Register Order Repositories and Services
            builder.Services.AddScoped<IOrderRepositories, OrderRepositories>();
            builder.Services.AddScoped<IOrderServices, OrderService>();

            // Register Delivery Repositories and Services
            builder.Services.AddScoped<IDeliveryRepositories, DeliveryRepositories>();
            builder.Services.AddScoped<IDeliveryServices, DeliveryService>();

            // OpenAPI
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline
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