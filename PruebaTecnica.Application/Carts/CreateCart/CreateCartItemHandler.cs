using MediatR;
using PruebaTecnica.Domain.Interfaces;
using PruebaTecnica.Domain.Cart;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PruebaTecnica.Domain.Products;

namespace PruebaTecnica.Application.Carts.CreateCart
{
    public class CreateCartItemHandler : IRequestHandler<CreateCartItemCommand, CartDto>
    {
        private readonly IProductRepository _products;
        private readonly ICartRepository _cartRepo;

        public CreateCartItemHandler(IProductRepository products, ICartRepository cartRepo)
        {
            _products = products;
            _cartRepo = cartRepo;
        }

        public Task<CartDto> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            var product = _products.GetById(request.ProductId)
                          ?? throw new Domain.Exceptions.DomainValidationException(
                                $"Producto {request.ProductId} no existe.");

            var selections = request.Groups
                .SelectMany(g => g.Items.Select(i =>
                    new SelectedAttribute(g.GroupAttributeId, i.AttributeId, i.Quantity)))
                .ToList();

            var item = new CartItem(product, selections, request.Quantity);

            var cart = _cartRepo.Get();
            cart.AddItem(item);
            _cartRepo.Save(cart);

            var dto = new CartDto(cart);
            return Task.FromResult(dto);
        }
    }
}