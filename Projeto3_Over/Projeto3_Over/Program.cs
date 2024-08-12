
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Projeto3_Over.Data;
using Projeto3_Over.Models;
using Projeto3_Over.Repositorios;
using Projeto3_Over.Repositorios.Interfaces;
using Projeto3_Over.Validators;

namespace Projeto3_Over
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

            builder.Services.AddDbContext<Projeto3DBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
            );

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            builder.Services.AddTransient<IValidator<EmpresaModel>, EmpresaValidator>();
            builder.Services.AddTransient<IValidator<UsuarioModel>, UsuarioValidator>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
