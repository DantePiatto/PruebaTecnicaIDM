

using PruebaTecnica.Application.Abstractions.Messaging;

namespace PruebaTecnica.Application.Carts.ChangeCartItemQuantity
{
public record ChangeCartItemQuantityCommand(Guid CartItemId, int Quantity)
    : ICommand<Guid?>;
}
