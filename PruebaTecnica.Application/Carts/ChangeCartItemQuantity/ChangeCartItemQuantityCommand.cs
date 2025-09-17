using MediatR;
using PruebaTecnica.Domain.Carts;

namespace PruebaTecnica.Application.Carts.ChangeCartItemQuantity
{
public record ChangeCartItemQuantityCommand(Guid CartItemId, int? Quantity, int? Delta)
    : IRequest<CartDto>;
}
