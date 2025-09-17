namespace PruebaTecnica.Domain.Cart;

using PruebaTecnica.Domain.Cart;

public interface ICartRepository
{
    Cart Get();
    void Save(Cart cart);
    void Clear();
}