namespace PruebaTecnica.Domain.Carts;


public interface ICartRepository
{
    Cart Get();
    CartItem? GetOneCartItem(Guid idCartItem);
    void Save(CartItem cartItem);
    void DeleteCartItem(Guid idCartItem);
    void Clear();
    CartItem? GetItemById(Guid cartItemId);
}