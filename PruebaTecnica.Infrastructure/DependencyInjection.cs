
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Domain.Carts;
using PruebaTecnica.Domain.Products;
using PruebaTecnica.Infrastructure.Repositories;

namespace PruebaTecnica.Infrastructure;

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


        services.AddSingleton<IProductRepository>(sp =>
           {


               string rootPath = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName;

               string basePath = configuration["Catalog:BasePath"] ?? "FileDatabase";
               string fileName = configuration["Catalog:FileName"] ?? "entities.txt";


               var path = Path.Combine(rootPath + basePath, fileName);
               
               return new InMemoryProductRepository(path); // ctor (string jsonFilePath)
           });
        services.AddSingleton<ICartRepository, InMemoryCartRepository>();





        return services;
    }

}