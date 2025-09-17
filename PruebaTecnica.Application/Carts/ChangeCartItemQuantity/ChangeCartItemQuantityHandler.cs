using MediatR;
using PruebaTecnica.Domain.Carts;
using PruebaTecnica.Domain.Exceptions;

namespace PruebaTecnica.Application.Carts.ChangeCartItemQuantity
{public class ChangeCartItemQuantityHandler
    : IRequestHandler<ChangeCartItemQuantityCommand, CartDto>
{
    private readonly ICartRepository _cartRepo;
    public ChangeCartItemQuantityHandler(ICartRepository cartRepo) => _cartRepo = cartRepo;

    public Task<CartDto> Handle(ChangeCartItemQuantityCommand request, CancellationToken ct)
    {
        var cart = _cartRepo.Get();

        var item = cart.Items.FirstOrDefault(i => i.Id == request.CartItemId)
            ?? throw new DomainValidationException("El ítem no existe en el carrito.");

        if (request.Quantity is not null)
        {
            // absoluta (idempotente)
            item.ChangeQuantity(request.Quantity.Value);
        }
        else
        {
            // relativa (delta)
            var newQty = item.Quantity + request.Delta!.Value;
            item.ChangeQuantity(newQty); // valida >= 1
        }

        _cartRepo.Save(cart);
        return Task.FromResult(new CartDto(cart));
    }
}
}
