
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SB.PruebaTecnica.Domain.Interfaces;
using SB.PruebaTecnica.Domain.Products;
using SB.PruebaTecnica.Infrastructure.FileDatabase;
using SB.PruebaTecnica.Infrastructure.Repositories;

namespace SB.PruebaTecnica.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        // var catalogPath = Path.Combine(builder.Environment.ContentRootPath, "catalogo-producto.json");
        services.AddSingleton<FileDbContext>();

        services.AddSingleton<IProductRepository>(sp =>
           {
               var env = sp.GetRequiredService<IHostEnvironment>();
               // Permite override por appsettings (opcional)
               var configuredPath = configuration["Catalog:Path"];
               var path = string.IsNullOrWhiteSpace(configuredPath)
                   ? Path.Combine(env.ContentRootPath, "catalogo-producto.json")
                   : Path.IsPathRooted(configuredPath)
                       ? configuredPath
                       : Path.Combine(env.ContentRootPath, configuredPath);

               return new InMemoryProductRepository(path); // ctor (string jsonFilePath)
           });
        // services.AddSingleton<JwtService>();





        return services;
    }

}