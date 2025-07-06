using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Infrastructure.Data;
using PharmaDrop.Infrastructure.Implementition.Repositories;
using PharmaDrop.Infrastructure.Implementition.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PharmaDrop.Application.Contract.Interfaces;

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
            services.AddTransient<IQRcodeServices, QRcodeServices>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(t => t.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateLifetime = true,
                  ValidateAudience = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = configuration["Token:Issure"],
                  ValidAudience = configuration["Token:Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]))
              });
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}
