using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Domain.Products;

namespace Domain.Cart
{
    public class CartItem : Entity<Guid>
    {
        public Product Product { get; }
        public int Quantity { get; private set; }
        public IReadOnlyList<SelectedAttribute> Selections { get; }

        public CartItem(Product product, IEnumerable<SelectedAttribute> selections, int quantity = 1)
        {
            Id = Guid.NewGuid();
            Product = product;
            Selections = selections.ToList().AsReadOnly();
            ChangeQuantity(quantity <= 0 ? 1 : quantity);
            ValidateAgainstProduct();
        }

        public void ChangeQuantity(int newQty)
        {
            if (newQty < 1) throw new DomainValidationException("La cantidad del ítem debe ser al menos 1.");
            Quantity = newQty;
        }

        public decimal UnitPrice()
        {
            decimal impacts = 0m;
            foreach (var sel in Selections)
            {
                var group = Product.GetGroupOrThrow(sel.GroupAttributeId);
                var opt = group.GetOptionOrThrow(sel.AttributeId);
                impacts += opt.PriceImpactAmount * sel.Quantity;
            }
            return Math.Round(Product.BasePrice + impacts, 2);
        }

        public decimal TotalPrice() => Math.Round(UnitPrice() * Quantity, 2);

        private void ValidateAgainstProduct()
        {
            // Agrupar selección por grupo
            var byGroup = Selections
                .GroupBy(s => s.GroupAttributeId)
                .ToDictionary(g => g.Key, g => g.Select(x => (x.AttributeId, x.Quantity)).ToList());

            // 1) Validar grupos/atributos y reglas por grupo
            foreach (var kv in byGroup)
            {
                var group = Product.GetGroupOrThrow(kv.Key);
                group.ValidateSelection(kv.Value);
            }

            // 2) Asegurar que todos los grupos EQUAL_THAN se hayan enviado
            foreach (var mg in Product.Groups.Where(g => g.QuantityInfo.VerifyValue == VerifyValue.EqualThan))
            {
                if (!byGroup.ContainsKey(mg.Id ?? ""))
                    throw new DomainValidationException($"El grupo obligatorio '{mg.Name}' debe enviarse en la selección.");
            }
        }
    }
}