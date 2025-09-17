using PruebaTecnica.Application.Abstractions.Messaging;
using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Domain.Carts;
using PruebaTecnica.Domain.Exceptions;
using PruebaTecnica.Domain.Products;

namespace PruebaTecnica.Application.Carts.UpdateCart
{
    public class UpdateCartItemHandler : ICommandHandler<UpdateCartItemCommand, Guid?>
    {
        private readonly IProductRepository _products;
        private readonly ICartRepository _cartRepo;

        public UpdateCartItemHandler(IProductRepository products, ICartRepository cartRepo)
        {
            _products = products;
            _cartRepo = cartRepo;
        }

        public async Task<Result<Guid?>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var product = _products.GetById(request.ProductId);

                if (product is null)
                {
                    return await Task.FromResult(Result.Failure<Guid?>(ProductErrors.NotFound));
                }

                var cartItem = _cartRepo.GetItemById(request.CartItemId);
                
                if (cartItem is null)
                {
                    return await Task.FromResult(Result.Failure<Guid?>(CartErrors.NotFound));
                }

                var selections = request.Groups
                    .SelectMany(g => g.Items.Select(i =>
                        new SelectedAttribute(g.GroupAttributeId, i.AttributeId, i.Quantity)))
                    .ToList();

                foreach (var grou in request.Groups)
                {
                    var group = product.GetGroupOrThrow(grou.GroupAttributeId); 
                    var selectionForGroup = grou.Items
                        .Select(i => (i.AttributeId, i.Quantity))
                        .ToList();

                    group.ValidateSelection(selectionForGroup); 
                }

                if (request.Quantity <= 0)
                    throw new DomainValidationException("La cantidad del producto debe ser mayor a 0.");

                cartItem.ReplaceSelections(selections);   
                cartItem.ChangeQuantity(request.Quantity);


                _cartRepo.Update(cartItem);
                return await Task.FromResult(Result.Success(request?.CartItemId, Message.Update));


            }
            catch (DomainValidationException ex)
            {
                return await Task.FromResult(Result.Failure<Guid?>(new Error(400, ex.Message)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data);
                throw;
            }
        }
    }
}