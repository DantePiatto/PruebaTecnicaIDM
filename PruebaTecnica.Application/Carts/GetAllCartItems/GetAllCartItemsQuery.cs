

using PruebaTecnica.Application.Abstractions.Messaging;
using PruebaTecnica.Domain.Carts;


namespace PruebaTecnica.Application.Carts.GetAllCartItems;

public record GetAllCartItemsQuery() : ICommand<CartDto?>;
