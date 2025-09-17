namespace SB.PruebaTecnica.Infrastructure.Repositories;

using System.Text.Json;
using System.Text.Json.Serialization;
using SB.PruebaTecnica.Domain.Products;
using SB.PruebaTecnica.Domain.Products.dto;

public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

    public InMemoryProductRepository(string jsonFilePath)
    {
        // Configuración del deserializador
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true, // tolerante con comas finales
            NumberHandling = JsonNumberHandling.AllowReadingFromString // por si algún número viene como string
        };

        // Si tu archivo contiene un SOLO producto:
        var json = File.ReadAllText(jsonFilePath);
        var dto = JsonSerializer.Deserialize<ProductDto>(json, options);
            
        //todo : terminar esto
            // if (dto != null) _products.Add(ProductFactory.ToDomain(dto));

        // Si quisieras soportar una LISTA de productos:
        // var json = File.ReadAllText(jsonFilePath);
        // // Intenta como lista y si falla, intenta como uno solo
        // List<ProductDto>? list = null;
        // try
        // {
        //     list = JsonSerializer.Deserialize<List<ProductDto>>(json, options);
        // }
        // catch { /* ignore */ }

        // if (list != null && list.Any())
        // {
        //     // foreach (var dto in list)
        //     // {
        //     //     var p = ProductFactory.ToDomain(dto);
        //     //     _products.Add(p);
        //     // }
        // }
        // else
        // {
        //     // var dto = JsonSerializer.Deserialize<ProductDto>(json, options);
        //     // if (dto != null)
        //     // {
        //     //     var p = ProductFactory.ToDomain(dto);
        //     //     _products.Add(p);
        //     // }
        // }
    }

        public Product? GetById(long productId) => _products.FirstOrDefault(p => p.Id == productId);

        public IReadOnlyList<Product> GetAll() => _products.AsReadOnly();
    }