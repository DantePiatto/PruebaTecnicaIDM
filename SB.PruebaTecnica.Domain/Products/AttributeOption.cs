using SB.PruebaTecnica.Domain.Abstractions;

namespace SB.PruebaTecnica.Domain.Products

{
    public class AttributeOption : Entity<long>
    {
        public string Name { get; }
        public int DefaultQuantity { get; }
        public int MaxQuantity { get; }
        public decimal PriceImpactAmount { get; } // por unidad
        public bool IsRequired { get; }
        public string? NegativeAttributeId { get; }
        public int Order { get; }
        public bool IsActive { get; }

        public AttributeOption(
            long attributeId,
            string name,
            int defaultQuantity,
            int maxQuantity,
            decimal priceImpactAmount,
            bool isRequired,
            string? negativeAttributeId,
            int order,
            bool isActive = true)
        {
            Id = attributeId;
            Name = name;
            DefaultQuantity = defaultQuantity;
            MaxQuantity = maxQuantity;
            PriceImpactAmount = priceImpactAmount;
            IsRequired = isRequired;
            NegativeAttributeId = negativeAttributeId;
            Order = order;
            IsActive = isActive;
        }
    }
}