namespace PruebaTecnica.Domain.Products;

using PruebaTecnica.Domain.Products;

public interface IProductRepository
{
    Product? GetById(long productId);
    IReadOnlyList<Product> GetAll();
}