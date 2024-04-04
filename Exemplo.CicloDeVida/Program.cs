using Exemplo.CicloDeVida.Controllers;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Exemplo.CicloDeVida
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CicloDeVida", Version = "v1" });

                //propriedade da api / xml no debug de saida
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
                c.IncludeXmlComments(Path.Combine(basePath, fileName));
            });

            //http://localhost:5024/api/v1/CicloDeVidaId
            builder.Services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();
            builder.Services.AddScoped<IExemploScoped, ExemploCicloDeVida>();
            builder.Services.AddTransient<IExemploTransient, ExemploCicloDeVida>();





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