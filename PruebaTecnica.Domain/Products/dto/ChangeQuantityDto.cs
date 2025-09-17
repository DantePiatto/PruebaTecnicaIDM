public sealed class ChangeQuantityDto
{
    public Guid? CartItemId { get; set; }
    public int? Quantity { get; set; } 
    public int? Delta { get; set; }    
}
