namespace Domain.Cart
{
    public class SelectedAttribute
    {
        public string GroupAttributeId { get; }
        public long AttributeId { get; }
        public int Quantity { get; }

        public SelectedAttribute(string groupAttributeId, long attributeId, int quantity)
        {
            GroupAttributeId = groupAttributeId;
            AttributeId = attributeId;
            Quantity = quantity;
        }
    }
}