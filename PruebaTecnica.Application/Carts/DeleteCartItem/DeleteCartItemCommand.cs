

using PruebaTecnica.Application.Abstractions.Messaging;

namespace PruebaTecnica.Application.Carts.DeleteCartItem;
public record DeleteCartItemCommand(
    Guid CartItemId
) : ICommand<Guid?>;
