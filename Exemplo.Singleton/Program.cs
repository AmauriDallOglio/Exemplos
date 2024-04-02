using Exemplo.Infra.Virtual;
using Microsoft.OpenApi.Models;

namespace Exemplo.Singleton
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
 
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Singleton",
                    Description = "API -  ",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "Exemplo.Singleton.xml");
                c.IncludeXmlComments(apiPath);
            });

            builder.Services.AddSingleton<SingletonContainer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Singleton");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}