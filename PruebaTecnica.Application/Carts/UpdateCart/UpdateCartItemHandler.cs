using PruebaTecnica.Application.Abstractions.Messaging;
using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Domain.Carts;
using PruebaTecnica.Domain.Exceptions;
using PruebaTecnica.Domain.Products;

namespace PruebaTecnica.Application.Carts.UpdateCart
{
    public class UpdateCartItemHandler : ICommandHandler<UpdateCartItemCommand, CartDto?>
    {
        private readonly IProductRepository _products;
        private readonly ICartRepository _cartRepo;

        public UpdateCartItemHandler(IProductRepository products, ICartRepository cartRepo)
        {
            _products = products;
            _cartRepo = cartRepo;
        }

        public async Task<Result<CartDto?>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var product = _products.GetById(request.ProductId);
                if (product is null)
                {
                    return await Task.FromResult(Result.Failure<CartDto>(ProductErrors.NotFound));
                }

                var cartItem = _cartRepo.GetItemById(request.CartItemId);
                if (cartItem is null)
                    throw new DomainValidationException("El ítem no existe en el carrito.");

                // 3) Construir nuevas selecciones desde el request
                var selections = request.Groups
                    .SelectMany(g => g.Items.Select(i =>
                        new SelectedAttribute(g.GroupAttributeId, i.AttributeId, i.Quantity)))
                    .ToList();

                // 4) Validaciones (idénticas a Add)
                // 4.1 Por cada grupo enviado: existencia + cantidades por grupo/atributo
                foreach (var grou in request.Groups)
                {
                    var group = product.GetGroupOrThrow(grou.GroupAttributeId); // lanza si no existe
                    var selectionForGroup = grou.Items
                        .Select(i => (i.AttributeId, i.Quantity))
                        .ToList();

                    group.ValidateSelection(selectionForGroup); // incluye total de grupo y max por atributo
                }

                // 6. Validar cantidad del ítem
                if (request.Quantity <= 0)
                    throw new DomainValidationException("La cantidad del producto debe ser mayor a 0.");

                // 7. Aplicar cambios al CartItem existente (solo si todo lo anterior pasó)
                cartItem.ReplaceSelections(selections);   // asegúrate de tener este método en CartItem
                cartItem.ChangeQuantity(request.Quantity);

                // 8. Guardar carrito y devolver DTO
                var cart = _cartRepo.Get();
                _cartRepo.Save(cart);

                var dto = new CartDto(cart);
                return await Task.FromResult(Result.Success(dto, Message.Update));


            }
            catch (DomainValidationException ex)
            {
                return await Task.FromResult(Result.Failure<CartDto>(new Error(400, ex.Message)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Data);
                throw;
            }
        }
    }
}