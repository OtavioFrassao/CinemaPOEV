using AutoMapper;
using CinemaDomain;
using CinemaRepository.Base;
using CinemaRepository.Context;
using CinemaService.Base;
using CinemaService.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;



namespace CinemaTest
{
    [TestClass]
    public class TestCinemaService
    {
        private ServiceCollection services;
        public ServiceProvider ConfigureServices()
        {
            var conStr = $"Host=localhost;Port=5432;Database=CinemaDB;"+"Username=postgres;Password=ifsp";
            services = new ServiceCollection();
            services.AddDbContext<MyDBContext>(options => options.UseNpgsql(conStr));

            services.AddScoped<IBaseRepository<Genero>, BaseRepository<Genero>>();
            services.AddScoped<IBaseService<Genero>, BaseService<Genero>>();

            services.AddScoped<IBaseRepository<Filme>, BaseRepository<Filme>>();
            services.AddScoped<IBaseService<Filme>, BaseService<Filme>>();

            services.AddSingleton(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Genero, Genero>();
                cfg.CreateMap<Filme, Filme>();

            },NullLoggerFactory.Instance).CreateMapper());

            return services.BuildServiceProvider();
        }

        [TestMethod]
        public void TestGeneroService() 
        { 
            var serviceProvider = ConfigureServices();
            var generoService = serviceProvider.GetService<IBaseService<Genero>>();
            var genero = new Genero { Id = 4, Nome = "Romance" };
            var result = generoService.Create<Genero, Genero, GeneroValidator>(genero);
            Console.WriteLine(JsonSerializer.Serialize(result));

        }

        [TestMethod]

        public void TestFilmeService()
        {
            var serviceProvider = ConfigureServices();
            var filmeService = serviceProvider.GetService<IBaseService<Filme>>();

            var generoExistente = new Genero { Id = 1, Nome = "Comédia" };
            var filme = new Filme { Id = 3, Nome = "Gente Grande 2", Classificacao = "+14", Duracao = 95, Genero = generoExistente };
            var result = filmeService.Create<Filme, Filme, FilmeValidator>(filme);
            Console.WriteLine(JsonSerializer.Serialize(result));
        }

    }
}
