using MediatR;
using PruebaTecnica.Domain.Carts;
using System.Linq;
using PruebaTecnica.Domain.Products;
using PruebaTecnica.Domain.Exceptions;
using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Application.Abstractions.Messaging;


namespace PruebaTecnica.Application.Carts.CreateCart
{
    public class CreateCartItemHandler : ICommandHandler<CreateCartItemCommand, CartDto?>
    {
        private readonly IProductRepository _products;
        private readonly ICartRepository _cartRepo;

        public CreateCartItemHandler(IProductRepository products, ICartRepository cartRepo)
        {
            _products = products;
            _cartRepo = cartRepo;
        }

        public async Task<Result<CartDto?>> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var product = _products.GetById(request.ProductId);

                if (product == null)
                {
                    return await Task.FromResult(Result.Failure<CartDto>(ProductErrors.NotFound));
                }

                var selections = request.Groups
                    .SelectMany(g => g.Items.Select(i =>
                        new SelectedAttribute(g.GroupAttributeId, i.AttributeId, i.Quantity)))
                    .ToList();



                //validacion

                foreach (var grou in request.Groups)
                {
                    var group = product.GetGroupOrThrow(grou.GroupAttributeId);

                    var selectionForGroup = grou.Items
                       .Select(i => (i.AttributeId, i.Quantity))
                       .ToList();

                    group.ValidateSelection(selectionForGroup);
                }

                if (request.Quantity <= 0)
                {
                    throw new DomainValidationException("La cantidad del producto debe ser mayor a 0.");
                }


                var item = new CartItem(product, selections, request.Quantity);

                var cart = _cartRepo.Get();
                cart.AddItem(item);
                _cartRepo.Save(cart);

                var dto = new CartDto(cart);

                return await Task.FromResult(Result.Success(dto, Message.Create));


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