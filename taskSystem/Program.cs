using Microsoft.EntityFrameworkCore;
using taskSystem.Context;
using taskSystem.Repositories;
using taskSystem.Repositories.Interfaces;

namespace taskSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SystemTasksDBContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection"))
                     .UseInternalServiceProvider(builder.Services.BuildServiceProvider())
                );

            builder.Services.AddScoped<IUserRepositorie, UserRepositorie>();
            builder.Services.AddScoped<ITaskRepositorie, TaskRepositorie>();

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
        }
    }
}