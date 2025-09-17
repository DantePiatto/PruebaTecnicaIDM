namespace SB.PruebaTecnica.Domain.Products;

using SB.PruebaTecnica.Domain.Products;

public interface IProductRepository
{
    Product? GetById(long productId);
    IReadOnlyList<Product> GetAll();
}