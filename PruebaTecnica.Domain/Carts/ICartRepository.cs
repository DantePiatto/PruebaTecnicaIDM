namespace PruebaTecnica.Domain.Carts;


public interface ICartRepository
{
    Cart Get();
    void Save(Cart cart);
    void Clear();
    CartItem? GetItemById(Guid cartItemId);
}