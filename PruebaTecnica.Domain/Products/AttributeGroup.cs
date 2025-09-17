using System.Collections.Generic;
using System.Linq;
using PruebaTecnica.Domain.Exceptions;
using PruebaTecnica.Domain.Abstractions;

namespace PruebaTecnica.Domain.Products
{
    public class AttributeGroup : Entity<string>
    {
        public string Name { get; }
        public string? Description { get; }
        public int Order { get; }
        public QuantityInformation QuantityInfo { get; }
        public IReadOnlyList<AttributeOption> Options { get; }

        public AttributeGroup(
            string groupAttributeId,
            string name,
            QuantityInformation quantityInfo,
            IEnumerable<AttributeOption> options,
            int order = 0,
            string? description = null)
        {
            Id = groupAttributeId;
            Name = name;
            QuantityInfo = quantityInfo;
            Options = options.ToList().AsReadOnly();
            Order = order;
            Description = description;
        }

        public AttributeOption GetOptionOrThrow(long attributeId)
        {
            var opt = Options.FirstOrDefault(o => o.Id == attributeId);
            if (opt is null)
                throw new DomainValidationException($"El atributo {attributeId} no pertenece al grupo {Id}.");
            return opt;
        }

        /// <summary>Valida una selección de (attributeId, quantity) contra las reglas del grupo.</summary>
        public void ValidateSelection(IReadOnlyList<(long attributeId, int quantity)> selection)
        {
            // 1) Consistencia de atributos
            foreach (var (attributeId, _) in selection) GetOptionOrThrow(attributeId);

            // 2) Cantidad por atributo
            foreach (var (attributeId, qty) in selection)
            {
                var opt = GetOptionOrThrow(attributeId);
                if (qty < 0 || qty > opt.MaxQuantity)
                    throw new DomainValidationException(
                        $"Cantidad {qty} inválida para atributo {opt.Name} (max {opt.MaxQuantity}).");
            }

            // 3) Cantidad total del grupo
            var totalQty = selection.Sum(s => s.quantity);
            if (QuantityInfo.VerifyValue == VerifyValue.EqualThan && totalQty != QuantityInfo.GroupAttributeQuantity)
                throw new DomainValidationException(
                    $"El grupo {Name} requiere exactamente {QuantityInfo.GroupAttributeQuantity} selecciones, recibido {totalQty}.");

            if (QuantityInfo.VerifyValue == VerifyValue.LowerEqualThan && totalQty > QuantityInfo.GroupAttributeQuantity)
                throw new DomainValidationException(
                    $"El grupo {Name} permite hasta {QuantityInfo.GroupAttributeQuantity} selecciones, recibido {totalQty}.");

            // 4) Requeridos (simple): si un atributo es requerido, debe venir con qty > 0
            foreach (var req in Options.Where(o => o.IsRequired))
            {
                var selQty = selection.FirstOrDefault(s => s.attributeId == req.Id).quantity;
                if (selQty <= 0)
                    throw new DomainValidationException($"El atributo requerido '{req.Name}' debe ser seleccionado.");
            }
        }
    }
}