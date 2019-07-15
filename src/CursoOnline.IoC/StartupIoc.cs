using CursoOnline.Dados.Contextos;
using CursoOnline.Dados.Repositorios;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.IoC
{
    public static class StartupIoc
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(configuration["ConnectionString"]));
            services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            services.AddScoped(typeof(ICursoRepositorio), typeof(CursoRepositorio));
            services.AddScoped<ArmazenadorDeCurso>();
        }
    }
}
