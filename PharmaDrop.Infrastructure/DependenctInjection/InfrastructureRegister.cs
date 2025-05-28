using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Infrastructure.Data;
using PharmaDrop.Infrastructure.Implementition.Repositories;
using PharmaDrop.Infrastructure.Implementition.Services;


namespace PharmaDrop.Infrastructure.DependenctInjection
{
    public static class  InfrastructureRegister
    {
        public static IServiceCollection InfraRegister (this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(s => s.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddTransient<ISendEmailServices, SendEmailServices>();
            services.AddSingleton<IImageServices, ImageServices>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));
            return services;
        }
    }
}
