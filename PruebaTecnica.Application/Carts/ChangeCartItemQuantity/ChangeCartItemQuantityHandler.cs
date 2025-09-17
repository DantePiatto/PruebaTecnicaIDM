using MediatR;
using PruebaTecnica.Application.Abstractions.Messaging;
using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Domain.Carts;
using PruebaTecnica.Domain.Exceptions;
using PruebaTecnica.Domain.Products;

namespace PruebaTecnica.Application.Carts.ChangeCartItemQuantity
{
    public class ChangeCartItemQuantityHandler
    : ICommandHandler<ChangeCartItemQuantityCommand, Guid?>
    {
        private readonly ICartRepository _cartRepo;
        public ChangeCartItemQuantityHandler(ICartRepository cartRepo) => _cartRepo = cartRepo;

        public async Task<Result<Guid?>> Handle(ChangeCartItemQuantityCommand request, CancellationToken ct)
        {
            var cartItem = _cartRepo.GetOneCartItem(request.CartItemId);

            if (cartItem == null)
            {
                return await Task.FromResult(Result.Failure<Guid?>(CartErrors.NotFound));

            }

            _cartRepo.UpdateQuantityCartItem(request.CartItemId, request.Quantity);

            return await Task.FromResult(Result.Success(request?.CartItemId, Message.Update));

        }
    }
}
