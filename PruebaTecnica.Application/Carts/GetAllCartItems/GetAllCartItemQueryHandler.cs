using MediatR;
using PruebaTecnica.Domain.Carts;
using System.Linq;
using PruebaTecnica.Domain.Products;
using PruebaTecnica.Domain.Exceptions;
using PruebaTecnica.Domain.Abstractions;
using PruebaTecnica.Application.Abstractions.Messaging;


namespace PruebaTecnica.Application.Carts.GetAllCartItems;

public class GetAllCartItemsQueryHandler : ICommandHandler<GetAllCartItemsQuery, CartDto?>
{
    private readonly ICartRepository _cartRepo;

    public GetAllCartItemsQueryHandler(ICartRepository cartRepo)
    {
        // _products = products;
        _cartRepo = cartRepo;
    }

    public async Task<Result<CartDto?>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var cart = _cartRepo.Get();


            var dto = new CartDto(cart);

            return await Task.FromResult(Result.Success(dto, Message.Create));


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            throw;
        }

    }
}