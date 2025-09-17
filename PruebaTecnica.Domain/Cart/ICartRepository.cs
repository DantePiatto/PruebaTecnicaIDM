namespace PruebaTecnica.Domain.Cart;

using PruebaTecnica.Domain.Cart;

public interface ICartRepository
{
    Cart? GetById(long productId);
    IReadOnlyList<Cart> GetAll();
    void Save(Cart cart);
    Cart Get();
}