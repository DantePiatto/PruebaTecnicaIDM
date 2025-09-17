using System;
using System.Collections.Generic;
using System.Linq;
using PruebaTecnica.Domain.Abstractions;

namespace PruebaTecnica.Domain.Cart
{
    public class Cart : Entity<Guid>
    {
        private readonly List<CartItem> _items = new();
        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

        public void AddItem(CartItem item)
        {
            // Merge si es el mismo producto con mismas selecciones
            var existing = _items.FirstOrDefault(i =>
                i.Product.Id == item.Product.Id &&
                i.Selections.OrderBy(s => s.GroupAttributeId).ThenBy(s => s.AttributeId)
                 .SequenceEqual(item.Selections.OrderBy(s => s.GroupAttributeId).ThenBy(s => s.AttributeId)));

            if (existing is null) _items.Add(item);
            else existing.ChangeQuantity(existing.Quantity + item.Quantity);
        }

        public void Remove(Guid cartItemId) => _items.RemoveAll(i => i.Id == cartItemId);

        public void ChangeItemQuantity(Guid cartItemId, int delta)
        {
            var item = _items.First(i => i.Id == cartItemId);
            item.ChangeQuantity(item.Quantity + delta);
        }

        public decimal Total() => Math.Round(_items.Sum(i => i.TotalPrice()), 2);
    }
}