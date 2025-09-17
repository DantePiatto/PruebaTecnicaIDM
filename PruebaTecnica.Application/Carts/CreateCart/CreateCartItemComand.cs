using MediatR;
using PruebaTecnica.Domain.Cart;
using System.Collections.Generic;

namespace PruebaTecnica.Application.Carts.CreateCart
{
    public record CreateCartItemCommand(
        long ProductId,
        int Quantity,
        List<GroupSelection> Groups
    ) : IRequest<CartDto>;

    public record GroupSelection(string GroupAttributeId, List<ItemSelection> Items);
    public record ItemSelection(long AttributeId, int Quantity);
}