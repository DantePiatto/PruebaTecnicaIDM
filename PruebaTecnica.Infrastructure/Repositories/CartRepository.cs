
using PruebaTecnica.Domain.Carts;

namespace PruebaTecnica.Infrastructure.Repositories;

// using PruebaTecnica.Domain.Carts;

public class InMemoryCartRepository : ICartRepository
{
        private Cart _cart = new();

        public Cart Get() => _cart;

        public CartItem? GetOneCartItem(Guid IdCartItem) => _cart.Items.FirstOrDefault(x => x.Id == IdCartItem);

        public void DeleteCartItem(Guid idCartItem)
         => _cart.Remove(idCartItem);

        public void UpdateQuantityCartItem(Guid idCartItem, int quantity)
         => _cart.ChangeItemQuantity(idCartItem, quantity);

         public void Update(CartItem cartItem)
         => _cart.UpdateItem(cartItem);


        public void Save(CartItem cartItem) => _cart.AddItem(cartItem);

        public void Clear() => _cart = new Cart();
        public CartItem? GetItemById(Guid cartItemId)
        {
            return _cart.Items.FirstOrDefault(i => i.Id == cartItemId);
        }
}