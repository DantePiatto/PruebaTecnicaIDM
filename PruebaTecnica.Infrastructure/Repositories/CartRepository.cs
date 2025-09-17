
namespace PruebaTecnica.Infrastructure.Repositories;

using PruebaTecnica.Domain.Carts;

public class InMemoryCartRepository : ICartRepository
{
        private Cart _cart = new();

        public Cart Get() => _cart;

        public void Save(Cart cart) => _cart = cart;

        public void Clear() => _cart = new Cart();
        public CartItem? GetItemById(Guid cartItemId)
        {
            return _cart.Items.FirstOrDefault(i => i.Id == cartItemId);
        }
}