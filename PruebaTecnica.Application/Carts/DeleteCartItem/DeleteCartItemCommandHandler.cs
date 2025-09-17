using MediatR;
using PruebaTecnica.Domain.Carts;
using System.Linq;
using PruebaTecnica.Domain.Products;
using PruebaTecnica.Domain.Exceptions;
using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Application.Abstractions.Messaging;


namespace PruebaTecnica.Application.Carts.DeleteCartItem
{
    public class DeleteCartItemCommandHandler : ICommandHandler<DeleteCartItemCommand, Guid?>
    {
        private readonly ICartRepository _cartRepo;

        public DeleteCartItemCommandHandler(IProductRepository products, ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<Result<Guid?>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var cartItem = _cartRepo.GetOneCartItem(request.CartItemId);


                if (cartItem == null)
                {
                    return await Task.FromResult(Result.Failure<Guid?>(CartErrors.NotFound));

                }

                _cartRepo.DeleteCartItem(request.CartItemId);

                return await Task.FromResult(Result.Success(request?.CartItemId, Message.Delete));


            }
            catch (DomainValidationException ex)
            {
                return await Task.FromResult(Result.Failure<Guid?>(new Error(400, ex.Message)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}