

using PruebaTecnica.Domain.Products;
using PruebaTecnica.Domain.Products.dto;

namespace PruebaTecnica.Infrastructure.ProductsCatalog
{
    public static class ProductFactory
    {
        public static Product ToDomain(ProductDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var groups = (dto.GroupAttributes ?? new List<GroupAttributeDto>())
                // Filtrar grupos vacíos/null por si el JSON viene sucio
                .Where(g => g != null && !string.IsNullOrWhiteSpace(g.GroupAttributeId))
                .Select(ToDomainGroup)
                .ToList();

            return new Product(
                productId: dto.ProductId,
                name: dto.Name ?? string.Empty,
                basePrice: dto.Price,
                groups: groups
            );
        }

        private static AttributeGroup ToDomainGroup(GroupAttributeDto g)
        {
            // Nombre del grupo: usar el del type si llega
            var groupName = g.GroupAttributeType?.Name ?? string.Empty;

            var qInfo = ToDomainQuantityInfo(g.QuantityInformation);

            var options = (g.Attributes ?? new List<AttributeOptionDto>())
                .Where(a => a != null && a.AttributeId != 0)
                .Select(ToDomainOption)
                .ToList();

            return new AttributeGroup(
                groupAttributeId: g.GroupAttributeId!,
                name: groupName,
                quantityInfo: qInfo,
                options: options,
                order: g.Order,
                description: g.Description
            );
        }

        private static QuantityInformation ToDomainQuantityInfo(QuantityInformationDto? q)
        {
            if (q == null)
            {
                // Por robustez, crea un default permisivo (0, LOWER_EQUAL_THAN)
                return new QuantityInformation(
                    groupAttributeQuantity: 0,
                    verifyValue: VerifyValue.LowerEqualThan,
                    showPricePerProduct: false,
                    isShown: true,
                    isEditable: true,
                    isVerified: false
                );
            }

            return new QuantityInformation(
                groupAttributeQuantity: q.GroupAttributeQuantity,
                verifyValue: MapVerify(q.VerifyValue),
                showPricePerProduct: q.ShowPricePerProduct,
                isShown: q.IsShown,
                isEditable: q.IsEditable,
                isVerified: q.IsVerified
            );
        }

        private static VerifyValue MapVerify(string? s) =>
            string.Equals(s, "EQUAL_THAN", StringComparison.OrdinalIgnoreCase)
                ? VerifyValue.EqualThan
                : VerifyValue.LowerEqualThan;

        private static AttributeOption ToDomainOption(AttributeOptionDto a)
        {
            // statusId "A" = activo; si no viene, lo consideramos activo
            var isActive = string.IsNullOrWhiteSpace(a.StatusId) || a.StatusId!.Equals("A", StringComparison.OrdinalIgnoreCase);

            return new AttributeOption(
                attributeId: a.AttributeId,
                name: a.Name ?? string.Empty,
                defaultQuantity: a.DefaultQuanIty,
                maxQuantity: a.MaxQuanIty,
                priceImpactAmount: a.PriceImpactAmount,
                isRequired: a.IsRequired,
                negativeAttributeId: a.NegativeAttributeId,
                order: a.Order,
                isActive: isActive
            );
        }
    }
}
