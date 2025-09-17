namespace PruebaTecnica.Domain.Products
{
    public class QuantityInformation
    {
        public int GroupAttributeQuantity { get; }
        public VerifyValue VerifyValue { get; }

        // Metadatos opcionales (por si los usas en UI)
        public bool ShowPricePerProduct { get; }
        public bool IsShown { get; }
        public bool IsEditable { get; }
        public bool IsVerified { get; }

        public QuantityInformation(
            int groupAttributeQuantity,
            VerifyValue verifyValue,
            bool showPricePerProduct = true,
            bool isShown = true,
            bool isEditable = true,
            bool isVerified = true)
        {
            if (groupAttributeQuantity < 0) throw new ArgumentOutOfRangeException(nameof(groupAttributeQuantity));
            GroupAttributeQuantity = groupAttributeQuantity;
            VerifyValue = verifyValue;
            ShowPricePerProduct = showPricePerProduct;
            IsShown = isShown;
            IsEditable = isEditable;
            IsVerified = isVerified;
        }
    }
}