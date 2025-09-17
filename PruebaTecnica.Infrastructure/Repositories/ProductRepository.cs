namespace PruebaTecnica.Infrastructure.Repositories;

using System.Text.Json;
using System.Text.Json.Serialization;
using PruebaTecnica.Domain.Products;
using PruebaTecnica.Domain.Products.dto;
using PruebaTecnica.Infrastructure.ProductsCatalog;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public InMemoryProductRepository(string jsonFilePath)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true, 
            NumberHandling = JsonNumberHandling.AllowReadingFromString 
        };

        var json = File.ReadAllText(jsonFilePath);
        var dto = JsonSerializer.Deserialize<ProductDto>(json, options);

        if (dto != null) _products.Add(ProductFactory.ToDomain(dto));


    }

    public Product? GetById(long productId) => _products.FirstOrDefault(p => p.Id == productId);

    public IReadOnlyList<Product> GetAll() => _products.AsReadOnly();
}