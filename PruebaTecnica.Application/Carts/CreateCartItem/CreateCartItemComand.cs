

using PruebaTecnica.Application.Abstractions.Messaging;
using PruebaTecnica.Domain.Carts;

namespace PruebaTecnica.Application.Carts.CreateCartItem
{
    public record CreateCartItemCommand(
        long ProductId,
        int Quantity,
        List<GroupSelection> Groups
    ) : ICommand<Guid?>;

    public record GroupSelection(string GroupAttributeId, List<ItemSelection> Items);
    public record ItemSelection(long AttributeId, int Quantity);
}