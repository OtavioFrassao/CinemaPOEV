using CinemaApp.Cadastros;
using CinemaDomain;
using CinemaRepository.Base;
using CinemaRepository.Context;
using CinemaService.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Infra
{
    public class ConfigureDI
    {
        public static ServiceCollection Services;
        public static IServiceProvider ServiceProvider;

        public static void Configure()
        {
            Services = new ServiceCollection();

            #region Banco de Dados Injeção de Dependencias
            var strCon = File.ReadAllText("Config/ConfigDB.txt");
            Services.AddDbContext<MyDBContext>(options => options.UseNpgsql(strCon));
            #endregion


            #region Repositorio Injeção de dependencia
            Services.AddScoped<IBaseRepository<Genero>, BaseRepository<Genero>>();
            Services.AddScoped<IBaseRepository<Filme>, BaseRepository<Filme>>();
            Services.AddScoped<IBaseRepository<Sala>, BaseRepository<Sala>>();
            Services.AddScoped<IBaseRepository<Sessao>, BaseRepository<Sessao>>();
            Services.AddScoped<IBaseRepository<Ingresso>, BaseRepository<Ingresso>>();
            #endregion

            #region Serviços injeção de dependencia
            Services.AddScoped<IBaseService<Genero>, BaseService<Genero>>();
            Services.AddScoped<IBaseService<Filme>, BaseService<Filme>>();
            Services.AddScoped<IBaseService<Sala>, BaseService<Sala>>();
            Services.AddScoped<IBaseService<Sessao>, BaseService<Sessao>>();
            Services.AddScoped<IBaseService<Ingresso>, BaseService<Ingresso>>();
            #endregion

            #region AutoMapper Injeção de Dependencia
            Services.AddSingleton(new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Genero, Genero>();
                cfg.CreateMap<Filme, Filme>();
                cfg.CreateMap<Sala, Sala>();
                cfg.CreateMap<Sessao, Sessao>();
                cfg.CreateMap<Ingresso, Ingresso>();
            }, NullLoggerFactory.Instance).CreateMapper());
            #endregion

            Services.AddTransient<CadastroGenero>();

            ServiceProvider = Services.BuildServiceProvider();
        }
    }
}
