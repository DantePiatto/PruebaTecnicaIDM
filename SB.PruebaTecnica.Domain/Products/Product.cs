using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
using SB.PruebaTecnica.Domain.Abstractions;

namespace Domain.Products
{
    public class Product : Entity<long>
    {
        public string Name { get; }
        public decimal BasePrice { get; } // precio del producto sin personalizaciones
        public IReadOnlyList<AttributeGroup> Groups { get; }

        public Product(long productId, string name, decimal basePrice, IEnumerable<AttributeGroup> groups)
        {
            Id = productId;
            Name = name;
            BasePrice = basePrice;
            Groups = groups.ToList().AsReadOnly();
        }

        public AttributeGroup GetGroupOrThrow(string groupId)
        {
            var g = Groups.FirstOrDefault(g => g.Id == groupId);
            if (g is null)
                throw new DomainValidationException($"El grupo {groupId} no existe en el producto {Id}.");
            return g;
        }
    }
}