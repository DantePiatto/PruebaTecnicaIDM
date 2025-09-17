

namespace PruebaTecnica.Domain.Carts
{
    public class CartDto
    {
        public List<CartItemDto> Items { get; set; }
        public decimal Total { get; set; }

        public CartDto(Cart cart)
        {
            Items = cart.Items.Select(i => new CartItemDto(i)).ToList();
            Total = cart.Total();
        }
    }

    public class CartItemDto
    {
        public Guid CartItemId { get; }
        public long ProductId { get; }
        public string ProductName { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public decimal TotalPrice { get; }
        public IReadOnlyList<SelectedAttribute> Selections { get; }

        public CartItemDto(CartItem item)
        {
            CartItemId = item.Id;
            ProductId = item.Product.Id;
            ProductName = item.Product.Name;
            Quantity = item.Quantity;
            UnitPrice = item.UnitPrice();
            TotalPrice = item.TotalPrice();
            Selections = item.Selections;
        }
    }
}