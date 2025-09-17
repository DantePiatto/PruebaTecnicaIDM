

using PruebaTecnica.Application.Abstractions.Messaging;
using PruebaTecnica.Domain.Carts;

namespace PruebaTecnica.Application.Carts.CreateCart
{
    public record CreateCartItemCommand(
        long ProductId,
        int Quantity,
        List<GroupSelection> Groups
    ) : ICommand<CartDto?>;

    public record GroupSelection(string GroupAttributeId, List<ItemSelection> Items);
    public record ItemSelection(long AttributeId, int Quantity);
}