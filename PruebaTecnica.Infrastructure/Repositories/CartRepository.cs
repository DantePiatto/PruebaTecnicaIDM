namespace PruebaTecnica.Infrastructure.Repositories;
using PruebaTecnica.Domain.Cart;

public class InMemoryCartRepository : ICartRepository
{
       private Cart _cart = new();

        public Cart Get() => _cart;

        public void Save(Cart cart) => _cart = cart;

        public void Clear() => _cart = new Cart();
}